BIN=bin/Release/osx-arm64/publish/fourcc

$(BIN): $(wildcard *.cs *.csproj)
	dotnet publish /p:SelfContained=true /p:PublishAot=true /p:PublishTrimmed=true

all: $(BIN)

install: $(BIN)
	ln -fs $(PWD)/fourcc ~/bin/fourcc
