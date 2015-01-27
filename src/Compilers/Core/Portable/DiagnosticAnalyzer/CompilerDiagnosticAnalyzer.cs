﻿// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;

namespace Microsoft.CodeAnalysis.Diagnostics
{
    /// <summary>
    /// DiagnosticAnalyzer for compiler's syntax/semantic/compilation diagnostics.
    /// </summary>
    internal abstract partial class CompilerDiagnosticAnalyzer : DiagnosticAnalyzer
    {
        internal abstract CommonMessageProvider MessageProvider { get; }
        internal abstract ImmutableArray<int> GetSupportedErrorCodes();

        public sealed override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics
        {
            get
            {
                // DiagnosticAnalyzer.SupportedDiagnostics should be invoked only once per analyzer, 
                // so we don't need to store the computed descriptors array into a field.

                var messageProvider = this.MessageProvider;
                var errorCodes = this.GetSupportedErrorCodes();
                var builder = ImmutableArray.CreateBuilder<DiagnosticDescriptor>(errorCodes.Length);
                foreach (var errorCode in errorCodes)
                {
                    var descriptor = DiagnosticInfo.GetDescriptor(errorCode, messageProvider);
                    builder.Add(descriptor);
                }

                return builder.ToImmutable();
            }
        }

        public sealed override void Initialize(AnalysisContext context)
        {
            context.RegisterCompilationStartAction(c =>
            {
                var analyzer = new CompilationAnalyzer(c.Compilation);
                c.RegisterSyntaxTreeAction(analyzer.AnalyzeSyntaxTree);
                c.RegisterSemanticModelAction(analyzer.AnalyzeSemanticModel);
            });

            context.RegisterCompilationEndAction(CompilationAnalyzer.AnalyzeCompilation);
        }
    }
}