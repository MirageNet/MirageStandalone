using System;
using Mono.Cecil;
using Mono.Cecil.Cil;
using MethodAttributes = Mono.Cecil.MethodAttributes;
using TypeAttributes = Mono.Cecil.TypeAttributes;

namespace Mirage.Weaver
{
    /// <summary>
    /// processes SyncVars, Cmds, Rpcs, etc. of NetworkBehaviours
    /// </summary>
    class NetworkBehaviourProcessor
    {
        public const string ProcessedFunctionName = "MirageProcessed";

        // by adding an empty MirageProcessed() function
        public static bool WasProcessed(TypeDefinition td)
        {
            return td.GetMethod(ProcessedFunctionName) != null;
        }

        public static void MarkAsProcessed(TypeDefinition td)
        {
            if (!WasProcessed(td))
            {
                MethodDefinition versionMethod = td.AddMethod(ProcessedFunctionName, MethodAttributes.Private);
                ILProcessor worker = versionMethod.Body.GetILProcessor();
                worker.Append(worker.Create(OpCodes.Ret));
            }
        }

        /// <summary>
        /// Adds code to static Constructor
        /// <para>
        /// If Constructor is missing a new one will be created
        /// </para>
        /// </summary>
        /// <param name="body">code to write</param>
        public static void AddToStaticConstructor(TypeDefinition typeDefinition, Action<ILProcessor> body)
        {
            MethodDefinition cctor = typeDefinition.GetMethod(".cctor");
            if (cctor != null)
            {
                // remove the return opcode from end of function. will add our own later.
                if (cctor.Body.Instructions.Count != 0)
                {
                    Instruction retInstr = cctor.Body.Instructions[cctor.Body.Instructions.Count - 1];
                    if (retInstr.OpCode == OpCodes.Ret)
                    {
                        cctor.Body.Instructions.RemoveAt(cctor.Body.Instructions.Count - 1);
                    }
                    else
                    {
                        throw new NetworkBehaviourException($"{typeDefinition.Name} has invalid static constructor", cctor, cctor.GetSequencePoint(retInstr));
                    }
                }
            }
            else
            {
                // make one!
                cctor = typeDefinition.AddMethod(".cctor", MethodAttributes.Private |
                        MethodAttributes.HideBySig |
                        MethodAttributes.SpecialName |
                        MethodAttributes.RTSpecialName |
                        MethodAttributes.Static);
            }

            ILProcessor worker = cctor.Body.GetILProcessor();

            // add new code to bottom of constructor
            // todo should we be adding new code to top of function instead? incase user has early return in custom constructor?
            body.Invoke(worker);

            // re-add return bececause we removed it earlier
            worker.Append(worker.Create(OpCodes.Ret));

            // in case class had no cctor, it might have BeforeFieldInit, so injected cctor would be called too late
            typeDefinition.Attributes &= ~TypeAttributes.BeforeFieldInit;
        }
    }
}
