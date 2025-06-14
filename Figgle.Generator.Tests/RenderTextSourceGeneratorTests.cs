﻿// Copyright Drew Noakes. Licensed under the Apache-2.0 license. See the LICENSE file for more details.

using System.Linq;
using Microsoft.CodeAnalysis;
using Xunit;

namespace Figgle.Generator.Tests;

public partial class RenderTextSourceGeneratorTests : SourceGeneratorTests
{
    protected override ISourceGenerator CreateSourceGenerator()
    {
        return new RenderTextSourceGenerator();
    }

    [Fact]
    public void SimpleCase_Class()
    {
        string source =
            """
            namespace Test.Namespace
            {
                [GenerateFiggleText("Member", "stacey", "Figgle")]
                internal partial class DemoUsage
                {
                }
            }
            """;
        string expected =
            """
            // Copyright Drew Noakes. Licensed under the Apache-2.0 license. See the LICENSE file for more details.

            // <auto-generated>
            //     This code was generated by Figgle.Generator.
            //
            //     Changes to this file may cause incorrect behavior and will be lost if
            //     the code is regenerated.
            // </auto-generated>

            namespace Test.Namespace
            {
                partial class DemoUsage
                {
                    public static string Member { get; } = @"_____________________________   _______
            7     77  77     77     77  7   7     7
            |  ___!|  ||   __!|   __!|  |   |  ___!
            |  __| |  ||  !  7|  !  7|  !___|  __|_
            |  7   |  ||     ||     ||     7|     7
            !__!   !__!!_____!!_____!!_____!!_____!
                                                   
            ";
                }
            }
            """;

        ValidateOutput(source, expected);
    }

    [Fact]
    public void SimpleCase_StaticClass()
    {
        string source =
            """
            namespace Test.Namespace
            {
                [GenerateFiggleText("Member", "stacey", "Figgle")]
                internal static partial class DemoUsage
                {
                }
            }
            """;
        string expected =
            """
            // Copyright Drew Noakes. Licensed under the Apache-2.0 license. See the LICENSE file for more details.

            // <auto-generated>
            //     This code was generated by Figgle.Generator.
            //
            //     Changes to this file may cause incorrect behavior and will be lost if
            //     the code is regenerated.
            // </auto-generated>

            namespace Test.Namespace
            {
                partial class DemoUsage
                {
                    public static string Member { get; } = @"_____________________________   _______
            7     77  77     77     77  7   7     7
            |  ___!|  ||   __!|   __!|  |   |  ___!
            |  __| |  ||  !  7|  !  7|  !___|  __|_
            |  7   |  ||     ||     ||     7|     7
            !__!   !__!!_____!!_____!!_____!!_____!
                                                   
            ";
                }
            }
            """;

        ValidateOutput(source, expected);
    }

    [Fact]
    public void SimpleCase_GlobalNamespace()
    {
        string source = """
            [GenerateFiggleText("Member", "stacey", "Figgle")]
            internal static partial class DemoUsage
            {
            }
            """;
        string expected =
            """
            // Copyright Drew Noakes. Licensed under the Apache-2.0 license. See the LICENSE file for more details.

            // <auto-generated>
            //     This code was generated by Figgle.Generator.
            //
            //     Changes to this file may cause incorrect behavior and will be lost if
            //     the code is regenerated.
            // </auto-generated>

                partial class DemoUsage
                {
                    public static string Member { get; } = @"_____________________________   _______
            7     77  77     77     77  7   7     7
            |  ___!|  ||   __!|   __!|  |   |  ___!
            |  __| |  ||  !  7|  !  7|  !___|  __|_
            |  7   |  ||     ||     ||     7|     7
            !__!   !__!!_____!!_____!!_____!!_____!
                                                   
            ";
                }

            """;

        ValidateOutput(source, expected);
    }

    [Fact]
    public void MultipleStrings()
    {
        string source =
            """
            namespace Test.Namespace
            {
                [GenerateFiggleText("HelloWorldString", "blocks", "Hello world")]
                [GenerateFiggleText("FiggleString", "stacey", "Figgle")]
                internal static partial class DemoUsage
                {
                }
            }
            """;
        string expected =
            """
            // Copyright Drew Noakes. Licensed under the Apache-2.0 license. See the LICENSE file for more details.

            // <auto-generated>
            //     This code was generated by Figgle.Generator.
            //
            //     Changes to this file may cause incorrect behavior and will be lost if
            //     the code is regenerated.
            // </auto-generated>

            namespace Test.Namespace
            {
                partial class DemoUsage
                {
                    public static string HelloWorldString { get; } = @" .----------------.  .----------------.  .----------------.  .----------------.  .----------------.   .----------------.  .----------------.  .----------------.  .----------------.  .----------------. 
            | .--------------. || .--------------. || .--------------. || .--------------. || .--------------. | | .--------------. || .--------------. || .--------------. || .--------------. || .--------------. |
            | |  ____  ____  | || |  _________   | || |   _____      | || |   _____      | || |     ____     | | | | _____  _____ | || |     ____     | || |  _______     | || |   _____      | || |  ________    | |
            | | |_   ||   _| | || | |_   ___  |  | || |  |_   _|     | || |  |_   _|     | || |   .'    `.   | | | ||_   _||_   _|| || |   .'    `.   | || | |_   __ \    | || |  |_   _|     | || | |_   ___ `.  | |
            | |   | |__| |   | || |   | |_  \_|  | || |    | |       | || |    | |       | || |  /  .--.  \  | | | |  | | /\ | |  | || |  /  .--.  \  | || |   | |__) |   | || |    | |       | || |   | |   `. \ | |
            | |   |  __  |   | || |   |  _|  _   | || |    | |   _   | || |    | |   _   | || |  | |    | |  | | | |  | |/  \| |  | || |  | |    | |  | || |   |  __ /    | || |    | |   _   | || |   | |    | | | |
            | |  _| |  | |_  | || |  _| |___/ |  | || |   _| |__/ |  | || |   _| |__/ |  | || |  \  `--'  /  | | | |  |   /\   |  | || |  \  `--'  /  | || |  _| |  \ \_  | || |   _| |__/ |  | || |  _| |___.' / | |
            | | |____||____| | || | |_________|  | || |  |________|  | || |  |________|  | || |   `.____.'   | | | |  |__/  \__|  | || |   `.____.'   | || | |____| |___| | || |  |________|  | || | |________.'  | |
            | |              | || |              | || |              | || |              | || |              | | | |              | || |              | || |              | || |              | || |              | |
            | '--------------' || '--------------' || '--------------' || '--------------' || '--------------' | | '--------------' || '--------------' || '--------------' || '--------------' || '--------------' |
             '----------------'  '----------------'  '----------------'  '----------------'  '----------------'   '----------------'  '----------------'  '----------------'  '----------------'  '----------------' 
            ";
                    public static string FiggleString { get; } = @"_____________________________   _______
            7     77  77     77     77  7   7     7
            |  ___!|  ||   __!|   __!|  |   |  ___!
            |  __| |  ||  !  7|  !  7|  !___|  __|_
            |  7   |  ||     ||     ||     7|     7
            !__!   !__!!_____!!_____!!_____!!_____!
                                                   
            ";
                }
            }
            """;

        ValidateOutput(source, expected);
    }

    [Fact]
    public void SimpleCase_TwoClasses_SameNamespace()
    {
        string source =
            """
            namespace Test.Namespace
            {
                [GenerateFiggleText("Member", "stacey", "Figgle")]
                internal partial class Demo1
                {
                }

                [GenerateFiggleText("Member", "stacey", "Figgle")]
                internal partial class Demo2
                {
                }
            }
            """;
        string expected1 =
            """
            // Copyright Drew Noakes. Licensed under the Apache-2.0 license. See the LICENSE file for more details.

            // <auto-generated>
            //     This code was generated by Figgle.Generator.
            //
            //     Changes to this file may cause incorrect behavior and will be lost if
            //     the code is regenerated.
            // </auto-generated>

            namespace Test.Namespace
            {
                partial class Demo1
                {
                    public static string Member { get; } = @"_____________________________   _______
            7     77  77     77     77  7   7     7
            |  ___!|  ||   __!|   __!|  |   |  ___!
            |  __| |  ||  !  7|  !  7|  !___|  __|_
            |  7   |  ||     ||     ||     7|     7
            !__!   !__!!_____!!_____!!_____!!_____!
                                                   
            ";
                }
            }
            """;
        string expected2 =
            """
            // Copyright Drew Noakes. Licensed under the Apache-2.0 license. See the LICENSE file for more details.

            // <auto-generated>
            //     This code was generated by Figgle.Generator.
            //
            //     Changes to this file may cause incorrect behavior and will be lost if
            //     the code is regenerated.
            // </auto-generated>

            namespace Test.Namespace
            {
                partial class Demo2
                {
                    public static string Member { get; } = @"_____________________________   _______
            7     77  77     77     77  7   7     7
            |  ___!|  ||   __!|   __!|  |   |  ___!
            |  __| |  ||  !  7|  !  7|  !___|  __|_
            |  7   |  ||     ||     ||     7|     7
            !__!   !__!!_____!!_____!!_____!!_____!
                                                   
            ";
                }
            }
            """;

        ValidateOutput(source, expected1, expected2);
    }

    [Fact]
    public void SimpleCase_TwoClasses_DifferentNamespace()
    {
        string source =
            """
            namespace Test.Namespace1
            {
                [GenerateFiggleText("Member", "stacey", "Figgle")]
                internal partial class Demo
                {
                }
            }

            namespace Test.Namespace2
            {
                [GenerateFiggleText("Member", "stacey", "Figgle")]
                internal partial class Demo
                {
                }
            }
            """;
        string expected1 =
            """
            // Copyright Drew Noakes. Licensed under the Apache-2.0 license. See the LICENSE file for more details.

            // <auto-generated>
            //     This code was generated by Figgle.Generator.
            //
            //     Changes to this file may cause incorrect behavior and will be lost if
            //     the code is regenerated.
            // </auto-generated>

            namespace Test.Namespace1
            {
                partial class Demo
                {
                    public static string Member { get; } = @"_____________________________   _______
            7     77  77     77     77  7   7     7
            |  ___!|  ||   __!|   __!|  |   |  ___!
            |  __| |  ||  !  7|  !  7|  !___|  __|_
            |  7   |  ||     ||     ||     7|     7
            !__!   !__!!_____!!_____!!_____!!_____!
                                                   
            ";
                }
            }
            """;
        string expected2 =
            """
            // Copyright Drew Noakes. Licensed under the Apache-2.0 license. See the LICENSE file for more details.

            // <auto-generated>
            //     This code was generated by Figgle.Generator.
            //
            //     Changes to this file may cause incorrect behavior and will be lost if
            //     the code is regenerated.
            // </auto-generated>

            namespace Test.Namespace2
            {
                partial class Demo
                {
                    public static string Member { get; } = @"_____________________________   _______
            7     77  77     77     77  7   7     7
            |  ___!|  ||   __!|   __!|  |   |  ___!
            |  __| |  ||  !  7|  !  7|  !___|  __|_
            |  7   |  ||     ||     ||     7|     7
            !__!   !__!!_____!!_____!!_____!!_____!
                                                   
            ";
                }
            }
            """;

        ValidateOutput(source, expected1, expected2);
    }

    [Fact]
    public void InvalidFontName()
    {
        string source =
            """
            namespace Test.Namespace
            {
                [GenerateFiggleText("Foo", "unknown-font", "Bar")]
                internal static partial class DemoUsage
                {
                }
            }
            """;

        var (_, diagnostics) = RunGenerator(source);

        var diagnostic = Assert.Single(diagnostics);

        Assert.Same(RenderTextSourceGenerator.UnknownFontNameDiagnostic, diagnostic.Descriptor);
        Assert.Equal("A font with name 'unknown-font' was not found", diagnostic.GetMessage());
    }

    [Fact]
    public void InvalidMemberName()
    {
        string source =
            """
            namespace Test.Namespace
            {
                [GenerateFiggleText("With Space", "stacey", "Foo")]
                internal static partial class DemoUsage
                {
                }
            }
            """;

        var (_, diagnostics) = RunGenerator(source);

        var diagnostic = Assert.Single(diagnostics);

        Assert.Same(RenderTextSourceGenerator.InvalidMemberNameDiagnostic, diagnostic.Descriptor);
        Assert.Equal("The string 'With Space' is not a valid member name", diagnostic.GetMessage());
    }

    [Fact]
    public void DuplicateMemberName_SamePart()
    {
        string source =
            """
            namespace Test.Namespace
            {
                [GenerateFiggleText("Foo", "stacey", "Foo")]
                [GenerateFiggleText("Foo", "stacey", "Foo")]
                internal static partial class DemoUsage
                {
                }
            }
            """;

        var (compilation, diagnostics) = RunGenerator(source);

        var diagnostic = Assert.Single(diagnostics);

        Assert.Same(RenderTextSourceGenerator.DuplicateMemberNameDiagnostic, diagnostic.Descriptor);
        Assert.Equal("Member 'Foo' has already been declared", diagnostic.GetMessage());

        string expected =
            """
            // Copyright Drew Noakes. Licensed under the Apache-2.0 license. See the LICENSE file for more details.

            // <auto-generated>
            //     This code was generated by Figgle.Generator.
            //
            //     Changes to this file may cause incorrect behavior and will be lost if
            //     the code is regenerated.
            // </auto-generated>

            namespace Test.Namespace
            {
                partial class DemoUsage
                {
                    public static string Foo { get; } = @"_____________________
            7     77     77     7
            |  ___!|  7  ||  7  |
            |  __| |  |  ||  |  |
            |  7   |  !  ||  !  |
            !__!   !_____!!_____!
                                 
            ";
                }
            }
            """;

        Assert.Equal(expected, compilation.SyntaxTrees.Last().ToString(), NewlineIgnoreComparer.Instance);
    }

    [Fact]
    public void MemberAlreadyExists_SamePart()
    {
        string source =
            """
            namespace Test.Namespace
            {
                [GenerateFiggleText("Foo", "stacey", "Foo")]
                internal static partial class DemoUsage
                {
                    public static void Foo() {}
                }
            }
            """;

        var (_, diagnostics) = RunGenerator(source);

        var diagnostic = Assert.Single(diagnostics);

        Assert.Same(RenderTextSourceGenerator.DuplicateMemberNameDiagnostic, diagnostic.Descriptor);
        Assert.Equal("Member 'Foo' has already been declared", diagnostic.GetMessage());
    }

    [Fact]
    public void MemberAlreadyExists_DifferentPart()
    {
        string source =
            """
            namespace Test.Namespace
            {
                [GenerateFiggleText("Foo", "stacey", "Foo")]
                internal static partial class DemoUsage
                {
                }

                internal static partial class DemoUsage
                {
                    public static void Foo() {}
                }
            }
            """;

        var (_, diagnostics) = RunGenerator(source);

        var diagnostic = Assert.Single(diagnostics);

        Assert.Same(RenderTextSourceGenerator.DuplicateMemberNameDiagnostic, diagnostic.Descriptor);
        Assert.Equal("Member 'Foo' has already been declared", diagnostic.GetMessage());
    }

    [Fact]
    public void DuplicateMemberName_DifferentParts()
    {
        string source =
            """
            namespace Test.Namespace
            {
                [GenerateFiggleText("Foo", "stacey", "Foo")]
                internal static partial class DemoUsage
                {
                }

                [GenerateFiggleText("Foo", "stacey", "Foo")]
                internal static partial class DemoUsage
                {
                }
            }
            """;

        var (compilation, diagnostics) = RunGenerator(source);

        var diagnostic = Assert.Single(diagnostics);

        Assert.Same(RenderTextSourceGenerator.DuplicateMemberNameDiagnostic, diagnostic.Descriptor);
        Assert.Equal("Member 'Foo' has already been declared", diagnostic.GetMessage());

        string expected =
            """
            // Copyright Drew Noakes. Licensed under the Apache-2.0 license. See the LICENSE file for more details.

            // <auto-generated>
            //     This code was generated by Figgle.Generator.
            //
            //     Changes to this file may cause incorrect behavior and will be lost if
            //     the code is regenerated.
            // </auto-generated>

            namespace Test.Namespace
            {
                partial class DemoUsage
                {
                    public static string Foo { get; } = @"_____________________
            7     77     77     7
            |  ___!|  7  ||  7  |
            |  __| |  |  ||  |  |
            |  7   |  !  ||  !  |
            !__!   !_____!!_____!
                                 
            ";
                }
            }
            """;

        Assert.Equal(expected, compilation.SyntaxTrees.Last().ToString(), NewlineIgnoreComparer.Instance);
    }

    [Fact]
    public void TypeIsNotPartial()
    {
        string source =
            """
            namespace Test.Namespace
            {
                [GenerateFiggleText("Foo", "stacey", "Foo")]
                internal class DemoUsage
                {
                }
            }
            """;

        var (_, diagnostics) = RunGenerator(source);

        var diagnostic = Assert.Single(diagnostics);

        Assert.Same(RenderTextSourceGenerator.TypeIsNotPartialDiagnostic, diagnostic.Descriptor);
        Assert.Equal("Type 'DemoUsage' must be partial", diagnostic.GetMessage());
    }

    [Fact]
    public void NestedTypeIsNotSupported()
    {
        string source =
            """
            namespace Test.Namespace
            {
                internal partial class Outer
                {
                    [GenerateFiggleText("Foo", "stacey", "Foo")]
                    internal partial class Inner
                    {
                    }
                }
            }
            """;

        var (_, diagnostics) = RunGenerator(source);

        var diagnostic = Assert.Single(diagnostics);

        Assert.Same(RenderTextSourceGenerator.NestedTypeIsNotSupportedDiagnostic, diagnostic.Descriptor);
        Assert.Equal("Unable to generate Figgle text for nested type 'Inner'. Generation is only supported for non-nested types.", diagnostic.GetMessage());
    }
}
