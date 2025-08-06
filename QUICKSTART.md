# Quick Start Guide

## Prerequisites
- Install [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

## Build & Run
```bash
# Clone repository
git clone https://github.com/terjeio/ioSender.git
cd ioSender

# Build and run
cd "ioSender/ioSender"
dotnet run
```

## Build for Distribution

### Windows
```bash
dotnet publish -c Release -r win-x64 --self-contained true -o ./dist/windows
```

### Linux  
```bash
dotnet publish -c Release -r linux-x64 --self-contained true -o ./dist/linux
```

### macOS
```bash
dotnet publish -c Release -r osx-x64 --self-contained true -o ./dist/macos
```

See [BUILD.md](BUILD.md) for complete documentation.