﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by Reqnroll (https://www.reqnroll.net/).
//      Reqnroll Version:2.0.0.0
//      Reqnroll Generator Version:2.0.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
using Reqnroll;
namespace AdvancedReqnRollTest.Features
{
    
    
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Reqnroll", "2.0.0.0")]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("CTM Temp Profile Page functionalities")]
    [NUnit.Framework.FixtureLifeCycleAttribute(NUnit.Framework.LifeCycle.InstancePerTestCase)]
    public partial class CTMTempProfilePageFunctionalitiesFeature
    {
        
        private global::Reqnroll.ITestRunner testRunner;
        
        private static string[] featureTags = ((string[])(null));
        
        private static global::Reqnroll.FeatureInfo featureInfo = new global::Reqnroll.FeatureInfo(new global::System.Globalization.CultureInfo("en-US"), "Features", "CTM Temp Profile Page functionalities", null, global::Reqnroll.ProgrammingLanguage.CSharp, featureTags);
        
#line 1 "TempProfile.feature"
#line hidden
        
        [NUnit.Framework.OneTimeSetUpAttribute()]
        public static async global::System.Threading.Tasks.Task FeatureSetupAsync()
        {
        }
        
        [NUnit.Framework.OneTimeTearDownAttribute()]
        public static async global::System.Threading.Tasks.Task FeatureTearDownAsync()
        {
        }
        
        [NUnit.Framework.SetUpAttribute()]
        public async global::System.Threading.Tasks.Task TestInitializeAsync()
        {
            testRunner = global::Reqnroll.TestRunnerManager.GetTestRunnerForAssembly(featureHint: featureInfo);
            try
            {
                if (((testRunner.FeatureContext != null) 
                            && (testRunner.FeatureContext.FeatureInfo.Equals(featureInfo) == false)))
                {
                    await testRunner.OnFeatureEndAsync();
                }
            }
            finally
            {
                if (((testRunner.FeatureContext != null) 
                            && testRunner.FeatureContext.BeforeFeatureHookFailed))
                {
                    throw new global::Reqnroll.ReqnrollException("Scenario skipped because of previous before feature hook error");
                }
                if ((testRunner.FeatureContext == null))
                {
                    await testRunner.OnFeatureStartAsync(featureInfo);
                }
            }
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public async global::System.Threading.Tasks.Task TestTearDownAsync()
        {
            if ((testRunner == null))
            {
                return;
            }
            try
            {
                await testRunner.OnScenarioEndAsync();
            }
            finally
            {
                global::Reqnroll.TestRunnerManager.ReleaseTestRunner(testRunner);
                testRunner = null;
            }
        }
        
        public void ScenarioInitialize(global::Reqnroll.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<NUnit.Framework.TestContext>(NUnit.Framework.TestContext.CurrentContext);
        }
        
        public async global::System.Threading.Tasks.Task ScenarioStartAsync()
        {
            await testRunner.OnScenarioStartAsync();
        }
        
        public async global::System.Threading.Tasks.Task ScenarioCleanupAsync()
        {
            await testRunner.CollectScenarioErrorsAsync();
        }
        
        public virtual async global::System.Threading.Tasks.Task FeatureBackgroundAsync()
        {
#line 3
    #line hidden
#line 4
        await testRunner.GivenAsync("the \"default\" user logged into \"default\" site", ((string)(null)), ((global::Reqnroll.Table)(null)), "Given ");
#line hidden
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Create a new temp record")]
        [NUnit.Framework.CategoryAttribute("criticalPath")]
        [NUnit.Framework.CategoryAttribute("Arun")]
        public async global::System.Threading.Tasks.Task CreateANewTempRecord()
        {
            string[] tagsOfScenario = new string[] {
                    "criticalPath",
                    "Arun"};
            global::System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new global::System.Collections.Specialized.OrderedDictionary();
            global::Reqnroll.ScenarioInfo scenarioInfo = new global::Reqnroll.ScenarioInfo("Create a new temp record", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 7
    this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((global::Reqnroll.TagHelper.ContainsIgnoreTag(scenarioInfo.CombinedTags) || global::Reqnroll.TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                await this.ScenarioStartAsync();
#line 3
    await this.FeatureBackgroundAsync();
#line hidden
#line 8
        await testRunner.GivenAsync("the user navigates to \'Temps\' tab", ((string)(null)), ((global::Reqnroll.Table)(null)), "Given ");
#line hidden
#line 9
        await testRunner.AndAsync("the user clicks \'New Temp link\'", ((string)(null)), ((global::Reqnroll.Table)(null)), "And ");
#line hidden
#line 10
        await testRunner.AndAsync("the user enters \'<unique_text>\' for \'temp first name\'", ((string)(null)), ((global::Reqnroll.Table)(null)), "And ");
#line hidden
#line 11
        await testRunner.AndAsync("the user enters \'<unique_text>\' for \'temp last name\'", ((string)(null)), ((global::Reqnroll.Table)(null)), "And ");
#line hidden
#line 12
        await testRunner.AndAsync("the user selects \'Active\' from the \'Status\' dropdown", ((string)(null)), ((global::Reqnroll.Table)(null)), "And ");
#line hidden
#line 13
        await testRunner.AndAsync("the user selects \'JasonTest\' from the \'HomeRegion\' dropdown", ((string)(null)), ((global::Reqnroll.Table)(null)), "And ");
#line hidden
#line 14
        await testRunner.AndAsync("the user clicks \'RN Cert\'", ((string)(null)), ((global::Reqnroll.Table)(null)), "And ");
#line hidden
#line 15
        await testRunner.AndAsync("the user clicks \'ER Spec\'", ((string)(null)), ((global::Reqnroll.Table)(null)), "And ");
#line hidden
                global::Reqnroll.Table table9 = new global::Reqnroll.Table(new string[] {
                            "AddressDetails",
                            "AddressValues"});
                table9.AddRow(new string[] {
                            "address",
                            "16801 Addison Road"});
                table9.AddRow(new string[] {
                            "city",
                            "Addison"});
                table9.AddRow(new string[] {
                            "state",
                            "TX"});
                table9.AddRow(new string[] {
                            "zip",
                            "75001"});
#line 16
        await testRunner.AndAsync("the user enters following address for \'temp-permanent\'", ((string)(null)), table9, "And ");
#line hidden
#line 22
        await testRunner.WhenAsync("the user clicks \'Temp Save\'", ((string)(null)), ((global::Reqnroll.Table)(null)), "When ");
#line hidden
#line 23
        await testRunner.ThenAsync("the user verifies the newly created temp ID is displayed", ((string)(null)), ((global::Reqnroll.Table)(null)), "Then ");
#line hidden
            }
            await this.ScenarioCleanupAsync();
        }
    }
}
#pragma warning restore
#endregion
