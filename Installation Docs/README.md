# Installing the NuGet package

Since the project is still in a very early state, I haven't yet provided a project template for easy setting up. That said, the setup process is simple enough:

Let's imagine we have a project called:

## RedBox
```bash
RedBox
├── RedBox
│   ├── Program.cs
│   ├── RedBox.csproj    *
│   ├── lib              *
│   ├── bin
│   └── obj
├── RedBox.sln
└── .gitignore
```
### Step 1
In order to run *RedBox*, you'll need to install [SDL2.NET](https://www.nuget.org/packages/SDL2.NET/) NuGet package into your project

### Step 2
Add the following to **RedBox.csproj**:
```xml
<Project>
```
```xml
	<!-- This copies all .dll files from the lib folder to the project output root folder -->
	<ItemGroup>
		<ContentWithTargetPath Include="lib\*.dll">
			<CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
			<TargetPath>%(Filename)%(Extension)</TargetPath>
		</ContentWithTargetPath>
		<None Include="lib\*.dll" />
	</ItemGroup>

	<!-- This copies all .txt files from the lib folder to the project output Licenses folder -->
	<ItemGroup>
		<ContentWithTargetPath Include="lib\*.txt">
			<CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
			<TargetPath>Licenses\%(Filename)%(Extension)</TargetPath>
		</ContentWithTargetPath>
		<None Include="lib\*.txt" />
	</ItemGroup>
```
```xml
</Project>
```
*Note: There are breaks in the code because you're not actually supposed to copy and paste the Project tags, but just put the big chunk inside it somewhere

### Step 3
You'll find, in this folder, a folder called "**Native**"; pick the platform(s) of your preference and drop the **lib** folder inside of your project's folder, as shown above.
And that's it! That should work!
If it doesn't please contact me! And have fun!