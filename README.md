# ncmsharp

## CMake

### Windows MSVC
 
```shell
cmake -G "Visual Studio 17 2022" -A x64 -B build

cmake --build build -j 8 --config MinSizeRel
```

### Windows MinGW

```shell
cmake -G "MinGW Makefiles" -DCMAKE_BUILD_TYPE=Release -B build

cmake --build build -j 8
```

### Linux / macOS

```shell
cmake -DCMAKE_BUILD_TYPE=Release -B build

cmake --build build -j 8
```
