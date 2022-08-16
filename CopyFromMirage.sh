MiragePath=$1

echo "Mirage Path $MiragePath" 

CopyScripts () {
    rm $2/*.cs
    # add -v to cp for verbose logging
    find $1/*.cs | xargs cp -t $2
}

CopyScripts "$MiragePath/Assets/Mirage/Runtime" "./Mirage/Runtime"

sed -i 's/"Unity\.Mirage\.CodeGen"/"Mirage.CodeGen"/g' ./Mirage/Runtime/AssemblyInfo.cs
echo "[assembly: InternalsVisibleTo(\"Mirage.Standalone\")]" >> ./Mirage/Runtime/AssemblyInfo.cs

CopyScripts "$MiragePath/Assets/Mirage/Runtime/Collections" "./Mirage/Runtime/Collections"
CopyScripts "$MiragePath/Assets/Mirage/Runtime/Events" "./Mirage/Runtime/Events"
CopyScripts "$MiragePath/Assets/Mirage/Runtime/RemoteCalls" "./Mirage/Runtime/RemoteCalls"
CopyScripts "$MiragePath/Assets/Mirage/Runtime/Serialization" "./Mirage/Runtime/Serialization"
CopyScripts "$MiragePath/Assets/Mirage/Runtime/Serialization/Packers" "./Mirage/Runtime/Serialization/Packers"

CopyScripts "$MiragePath/Assets/Mirage/Weaver" "./Mirage.CodeGen/Weaver"
rm "./Mirage.CodeGen/Weaver/MirageILPostProcessor.cs"
CopyScripts "$MiragePath/Assets/Mirage/Weaver/Processors" "./Mirage.CodeGen/Weaver/Processors"
CopyScripts "$MiragePath/Assets/Mirage/Weaver/Processors/NetworkBehaviour" "./Mirage.CodeGen/Weaver/Processors/NetworkBehaviour"
CopyScripts "$MiragePath/Assets/Mirage/Weaver/Processors/SyncVars" "./Mirage.CodeGen/Weaver/Processors/SyncVars"
CopyScripts "$MiragePath/Assets/Mirage/Weaver/Serialization" "./Mirage.CodeGen/Weaver/Serialization"

cp "$MiragePath/Assets/Mirage/Runtime/Logging/LogFactory.cs" "./Mirage.Logging/Logging/"
sed -i 's/new Logger(defaultLogHandler)/new StandaloneLogger()/g' ./Mirage.Logging/Logging/LogFactory.cs

CopyScripts "$MiragePath/Assets/Mirage/Runtime/SocketLayer" "./Mirage.SocketLayer/SocketLayer"
CopyScripts "$MiragePath/Assets/Mirage/Runtime/SocketLayer/Enums" "./Mirage.SocketLayer/SocketLayer/Enums"

CopyScripts "$MiragePath/Assets/Mirage/Runtime/Sockets/Udp" "./Mirage.Sockets/Sockets/Udp"
# CopyScripts "$MiragePath/Assets/Mirage/Runtime/Sockets/Udp/NanoSockets" "./Mirage.Sockets/Sockets/Udp/NanoSockets"
# CopyScripts "$MiragePath/Assets/Mirage/Runtime/Sockets/Udp/NanoSockets/Plugins" "./Mirage.Sockets/Sockets/Udp/NanoSockets/Plugins"
CopyScripts "$MiragePath/Assets/Mirage/Runtime/Sockets/Udp/NanoSockets/Scripts" "./Mirage.Sockets/Sockets/Udp/NanoSockets/Scripts"