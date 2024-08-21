# ncmsharp

## CMake

Under the directory `ncmdump`:

### Windows MSVC
 
```shell
cmake -G "Visual Studio 17 2022" -A x64 -B build

cmake --build build -j 8 --config MinSizeRel

copy .\build\MinSizeRel\ncmdump.lib ..\ncmdump_lib\
```

### Windows MinGW

```shell
cmake -G "MinGW Makefiles" -DCMAKE_BUILD_TYPE=Release -B build

cmake --build build -j 8

cp ./build/MinSizeRel/ncmdump.lib ../ncmdump_lib/
```

### Linux / macOS

```shell
cmake -DCMAKE_BUILD_TYPE=Release -B build

cmake --build build -j 8

cp ./build/MinSizeRel/ncmdump.lib ../ncmdump_lib/
```
