# Windows input regression checks

Run in a disposable C: Windows TEMP copy of the repository, using the Windows
.NET SDK, rather than building directly against WSL source:

```powershell
dotnet run --project tests/Repilot.InputTests/Repilot.InputTests.csproj -c Release
```

The test opens and closes its own text field. Keep that window focused during the
test. It refuses to inject keys into another foreground window.

Checks cover source-generated settings serialization, existing shortcut settings,
US and Portuguese key labels, real Unicode text delivery (including surrogate
pairs and spaces), empty actions, and existing shortcut delivery. Layout changes
apply only to the test thread and are restored afterward.

These checks do not establish physical Copilot-key activation or behavior in every
application. For an installed-package check, choose **Text**, enter `ç`, focus a
text field in another app, and press the Copilot key. Check that one press inserts
one character and that the Text action survives closing and reopening Settings.
