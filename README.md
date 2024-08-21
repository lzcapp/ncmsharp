# ncmsharp

## Build the Library File

Under the directory `ncmdump`:

### Windows MSVC
 
```shell
cmake -G "Visual Studio 17 2022" -A x64 -B build

cmake --build build -j 8 --config MinSizeRel

copy .\build\MinSizeRel\libncmdump.dll ..\ncmdump_lib\
```
