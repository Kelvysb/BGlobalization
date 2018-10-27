using Microsoft.VisualStudio.TestTools.UnitTesting;
using BGlobalization;
using BGlobalization.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BGlobalization.Tests
{
    [TestClass()]
    public class BGlobalizationTests
    {
        [TestMethod()]
        public void GetValueTest()
        {


            try
            {
                GenerateTestFiles();

                string test1 = BGLanguage.Instance.GetValue("test1");
                string test2 = BGLanguage.Instance.GetValue("test2");

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        private void GenerateTestFiles()
        {

            StreamWriter objAuxTestFile;
            BGLanguageSet objAuxTest;

            try
            {


                if (!File.Exists("pt-br.json"))
                {
                    objAuxTest = new BGLanguageSet();
                    objAuxTest.LanguageKey = "pt-br";
                    objAuxTest.Description = "Brazilian Portuguese.";
                    objAuxTest.Itens.Add(new BGLanguageItem());
                    objAuxTest.Itens.Last().Key = "test1";
                    objAuxTest.Itens.Last().Value = "Valor teste 1";
                    objAuxTest.Itens.Add(new BGLanguageItem());
                    objAuxTest.Itens.Last().Key = "test2";
                    objAuxTest.Itens.Last().Value = "test value2";
                    objAuxTestFile = new StreamWriter(objAuxTest.LanguageKey + ".json");
                    objAuxTestFile.Write(objAuxTest.Serialize());
                    objAuxTestFile.Close();
                    objAuxTestFile.Dispose();
                }

                if (!File.Exists("en-us.json"))
                {
                    objAuxTest = new BGLanguageSet();
                    objAuxTest.LanguageKey = "en-us";
                    objAuxTest.Description = "Us English.";
                    objAuxTest.Itens.Add(new BGLanguageItem());
                    objAuxTest.Itens.Last().Key = "test1";
                    objAuxTest.Itens.Last().Value = "test value1";
                    objAuxTest.Itens.Add(new BGLanguageItem());
                    objAuxTest.Itens.Last().Key = "test2";
                    objAuxTest.Itens.Last().Value = "test value2";
                    objAuxTestFile = new StreamWriter(objAuxTest.LanguageKey + ".json");
                    objAuxTestFile.Write(objAuxTest.Serialize());
                    objAuxTestFile.Close();
                    objAuxTestFile.Dispose();
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}