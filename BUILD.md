# ioSender Cross-Platform Build Guide

This guide explains how to build and run ioSender on Windows, Linux, and macOS after the migration to Avalonia UI framework.

## Prerequisites

### Required Software
- **.NET 8 SDK** or later
  - Download from: https://dotnet.microsoft.com/download/dotnet/8.0
- **Git** for cloning the repository

### Platform-Specific Requirements

#### Windows
- Windows 10 version 1903 or later
- Visual Studio 2022 (optional, for IDE development)

#### Linux
- Ubuntu 18.04+ / Debian 9+ / Fedora 32+ / OpenSUSE 15+
- Dependencies: `libx11-dev`, `libice6`, `libsm6`

#### macOS
- macOS 10.15 (Catalina) or later
- Xcode Command Line Tools (for native dependencies)

## Getting Started

### 1. Clone the Repository
```bash
git clone https://github.com/terjeio/ioSender.git
cd ioSender
```

### 2. Verify .NET Installation
```bash
dotnet --version
# Should show 8.0.x or later
```

## Building the Application

### Development Build
Build for your current platform:
```bash
# Navigate to the main project
cd "ioSender/ioSender"

# Restore dependencies and build
dotnet restore
dotnet build
```

### Running the Application
```bash
# From the ioSender/ioSender directory
dotnet run
```

## Cross-Platform Publishing

### Windows (x64)
```bash
# Self-contained executable (includes .NET runtime)
dotnet publish -c Release -r win-x64 --self-contained true -o ./publish/win-x64

# Framework-dependent (requires .NET 8 installed)
dotnet publish -c Release -r win-x64 --self-contained false -o ./publish/win-x64-fd
```

### Linux (x64)
```bash
# Self-contained executable
dotnet publish -c Release -r linux-x64 --self-contained true -o ./publish/linux-x64

# Framework-dependent
dotnet publish -c Release -r linux-x64 --self-contained false -o ./publish/linux-x64-fd
```

### macOS (x64)
```bash
# Self-contained executable
dotnet publish -c Release -r osx-x64 --self-contained true -o ./publish/osx-x64

# Framework-dependent  
dotnet publish -c Release -r osx-x64 --self-contained false -o ./publish/osx-x64-fd
```

### macOS (ARM64) - Apple Silicon
```bash
# Self-contained executable
dotnet publish -c Release -r osx-arm64 --self-contained true -o ./publish/osx-arm64

# Framework-dependent
dotnet publish -c Release -r osx-arm64 --self-contained false -o ./publish/osx-arm64-fd
```

## Alternative Runtime Identifiers

For other platforms, use these Runtime Identifiers (RID):
- `win-x86` - Windows 32-bit
- `win-arm64` - Windows ARM64
- `linux-arm` - Linux ARM32
- `linux-arm64` - Linux ARM64
- `linux-musl-x64` - Alpine Linux x64

## Avalonia-Specific Build Options

### Single File Deployment
Create a single executable file:
```bash
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -o ./publish/single-file
```

### Trimmed Build (Smaller Size)
Reduce output size by removing unused code:
```bash
dotnet publish -c Release -r linux-x64 --self-contained true -p:PublishTrimmed=true -o ./publish/trimmed
```

### ReadyToRun (Faster Startup)
Pre-compile for faster application startup:
```bash
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishReadyToRun=true -o ./publish/r2r
```

## Running Published Applications

### Windows
```bash
# Navigate to publish folder and run
cd ./publish/win-x64
./ioSender.exe
```

### Linux
```bash
# Make executable and run
cd ./publish/linux-x64
chmod +x ./ioSender
./ioSender
```

### macOS
```bash
# Make executable and run
cd ./publish/osx-x64
chmod +x ./ioSender
./ioSender
```

## Troubleshooting

### Linux Dependencies
If you encounter missing library errors on Linux:
```bash
# Ubuntu/Debian
sudo apt-get update
sudo apt-get install libx11-6 libice6 libsm6 libfontconfig1

# Fedora/CentOS/RHEL
sudo dnf install libX11 libICE libSM fontconfig
```

### macOS Permission Issues
If macOS blocks the application:
1. Right-click the executable and select "Open"
2. Or go to System Preferences → Security & Privacy → General → "Allow apps downloaded from"

### Build Errors
If you encounter build errors about duplicate compile items:
- This is a known issue during the migration process
- The errors are related to legacy project file configurations
- The build process is being refined to resolve these conflicts

## Development Environment Setup

### Visual Studio Code (Recommended)
1. Install the C# extension
2. Install the Avalonia for VSCode extension
3. Open the project folder in VS Code

### Visual Studio 2022 (Windows)
1. Install the Avalonia for Visual Studio extension
2. Open the solution file (if available) or the project folder

### JetBrains Rider
1. Install the Avalonia plugin
2. Open the project folder or solution file

## Project Structure

The main application components:
- `ioSender/ioSender/` - Main application project
- `CNC Core/` - Core CNC communication library
- `CNC Controls/` - UI controls library
- `CNC GCodeViewer/` - 3D GCode visualization
- `CNC Controls Probing/` - Probing functionality
- `CNC Controls Lathe/` - Lathe-specific controls
- `CNC Controls Camera/` - Camera integration
- `CNC Controls Dragknife/` - Dragknife support
- `CNC Converters/` - File format converters

## Performance Tips

### Release Builds
Always use Release configuration for distribution:
```bash
dotnet publish -c Release [other options]
```

### Self-Contained vs Framework-Dependent
- **Self-contained**: Larger file size but no .NET installation required
- **Framework-dependent**: Smaller file size but requires .NET 8 runtime on target machine

### Platform-Specific Optimizations
- Use `PublishTrimmed=true` for embedded/resource-constrained environments
- Use `PublishSingleFile=true` for easy distribution
- Use `PublishReadyToRun=true` for faster startup on same architecture

---

For additional support and updates, please visit the [ioSender GitHub repository](https://github.com/terjeio/ioSender).