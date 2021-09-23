using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace Mirage.Weaver
{
    public enum DiagnosticType : byte
    {
        Error,
        Assert,
        Warning,
        Log,
        Exception,
    }

    public class DiagnosticMessage
    {
        public DiagnosticMessage()
        { }

        public string File { get; set; }
        public DiagnosticType DiagnosticType { get; set; }
        public string MessageData { get; set; }
        public int Line { get; set; }
        public int Column { get; set; }

        public override string ToString() {
            return $"[{DiagnosticType}] in {File}:{Line}:{Column}: {MessageData}";
        }
    }

    public class WeaverLogger : IWeaverLogger
    {
        public List<DiagnosticMessage> Diagnostics = new List<DiagnosticMessage>();

        public void Error(string message)
        {
            AddMessage(message, null, DiagnosticType.Error);
        }

        public void Error(string message, MemberReference mr)
        {
            Error($"{message} (at {mr})");
        }

        public void Error(string message, MemberReference mr, SequencePoint sequencePoint)
        {
            AddMessage($"{message} (at {mr})", sequencePoint, DiagnosticType.Error);
        }

        public void Error(string message, MethodDefinition md)
        {
            Error(message, md, md.DebugInformation.SequencePoints.FirstOrDefault());
        }


        public void Warning(string message)
        {
            AddMessage($"{message}", null, DiagnosticType.Warning);
        }

        public void Warning(string message, MemberReference mr)
        {
            Warning($"{message} (at {mr})");
        }

        public void Warning(string message, MemberReference mr, SequencePoint sequencePoint)
        {
            AddMessage($"{message} (at {mr})", sequencePoint, DiagnosticType.Warning);
        }

        public void Warning(string message, MethodDefinition md)
        {
            Warning(message, md, md.DebugInformation.SequencePoints.FirstOrDefault());
        }


        private void AddMessage(string message, SequencePoint sequencePoint, DiagnosticType diagnosticType)
        {
            Diagnostics.Add(new DiagnosticMessage
            {
                DiagnosticType = diagnosticType,
                File = sequencePoint?.Document.Url.Replace($"{Environment.CurrentDirectory}{Path.DirectorySeparatorChar}", ""),
                Line = sequencePoint?.StartLine ?? 0,
                Column = sequencePoint?.StartColumn ?? 0,
                MessageData = message
            });
            
            Console.WriteLine(Diagnostics[^1]);
        }
    }
}
