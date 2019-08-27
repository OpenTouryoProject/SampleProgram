# 以下の様にしてWSLから実行する。改行コードはLFに変更してある。
#$ cd /mnt/c/Git1/SampleProgram/Other/BinarySerialize/TestBinarySerializeXplat
#$ ./BuildAndExec.sh

cd /mnt/c/Git1/SampleProgram/Other/BinarySerialize/TestBinarySerializeXplat/core20
dotnet publish -c Debug -r ubuntu.16.04-x64 --self-contained
cd bin/Debug/netcoreapp2.0/ubuntu.16.04-x64
dotnet TestBinarySerializeXplat.dll

cd /mnt/c/Git1/SampleProgram/Other/BinarySerialize/TestBinarySerializeXplat/core30
dotnet publish -c Debug -r ubuntu.16.04-x64 --self-contained
cd bin/Debug/netcoreapp3.0/ubuntu.16.04-x64
dotnet TestBinarySerializeXplat.dll
