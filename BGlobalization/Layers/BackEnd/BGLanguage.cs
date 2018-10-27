using BGlobalization.Basic;
using BGlobalization.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;
using System.Threading;

namespace BGlobalization
{
    public class BGLanguage
    {

        #region Declarations
        private static BGLanguage instance;
        private string strBasePath;
        private List<string> strLanguageSetsKeys;
        private List<BGLanguageSet> objLanguageSets;
        #endregion

        #region Constructor
        private BGLanguage(string p_strPath)
        {
            try
            {
                LoadFolder(p_strPath);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Functions
        private static BGLanguage GetInstance()
        {
            try
            {
                if (instance == null)
                {
                    instance = new BGLanguage("");
                }

                return instance;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void Initialize(string p_strPath)
        {
            try
            {
                instance = new BGLanguage(p_strPath);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void LoadFolder(string p_strPath)
        {

            string[] strFiles;
            StreamReader objfile;
            string strAuxFile;

            try
            {

                strBasePath = p_strPath;

                if(strBasePath == "")
                {
                    strBasePath = ".\\";
                }

                if (!strBasePath.EndsWith("\\"))
                {
                    strBasePath = strBasePath + "\\";
                }

                strFiles = Directory.GetFiles(strBasePath, "*.json");

                objLanguageSets = new List<BGLanguageSet>();
                strLanguageSetsKeys = new List<string>();

                foreach (string file in strFiles)
                {
                    try
                    {
                        objfile = new StreamReader(file);
                        strAuxFile = objfile.ReadToEnd();
                        objfile.Close();
                        objfile.Dispose();
                        objfile = null;
                        LanguageSets.Add(BGLanguageSet.Deserialize(strAuxFile));
                        strLanguageSetsKeys.Add(LanguageSets.Last().LanguageKey);
                    }
                    catch (Exception)
                    {

                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Save()
        {

            StreamWriter objfile;
            string strAuxPath;

            try
            {

                foreach (BGLanguageSet item in LanguageSets)
                {

                    strAuxPath = strBasePath + item.LanguageKey + ".json";

                    objfile = new StreamWriter(strAuxPath);
                    objfile.Write(item.Serialize());
                    objfile.Close();
                    objfile.Dispose();
                    objfile = null;

                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public string GetValue(string p_strKey, string p_strLanguageKey)
        {
            BGLanguageSet objAuxLanguageSet;

            try
            {

                objAuxLanguageSet = LanguageSets.Find((langSet) => { return langSet.LanguageKey.Equals(p_strLanguageKey, StringComparison.InvariantCultureIgnoreCase); });

                if (objAuxLanguageSet == null)
                {
                    objAuxLanguageSet = LanguageSets.Find((langSet) => { return langSet.Default; });
                }

                if (objAuxLanguageSet == null && LanguageSets.Count > 0)
                {
                    objAuxLanguageSet = LanguageSets.First();
                }
                else if(LanguageSets.Count == 0)
                {
                    throw new LanguageSetNotFoundException(p_strLanguageKey);
                }

                return objAuxLanguageSet.GetValue(p_strKey);

            }
            catch (Exception)
            {
                throw;
            }

        }

        public string GetValue(string p_strKey)
        {
            string strLanguageKey;
            CultureInfo currentCulture;

            try
            {

                currentCulture = Thread.CurrentThread.CurrentCulture;

                strLanguageKey = currentCulture.Name;

                return GetValue(p_strKey, strLanguageKey);

            }
            catch (Exception)
            {
                throw;
            }

        }

        public string GetValue(string p_strKey, CultureInfo p_objCulture)
        {
            try
            {       
                return GetValue(p_strKey, p_objCulture.Name);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public void AddLanguageSet(BGLanguageSet p_objLanguageSet)
        {
            try
            {
                if(LanguageSets.Find(item => { return item.LanguageKey.Equals(p_objLanguageSet.LanguageKey, StringComparison.InvariantCultureIgnoreCase); }) != null)
                {
                    throw new LanguageSetAlreadyExistsException(p_objLanguageSet.LanguageKey);
                }
                LanguageSets.Add(p_objLanguageSet);
                strLanguageSetsKeys.Add(p_objLanguageSet.LanguageKey);
            }
            catch (Exception)
            {
                throw;
            }

        }

        #endregion

        #region Properties
        public static BGLanguage Instance { get => GetInstance(); }
        public List<BGLanguageSet> LanguageSets { get => objLanguageSets; }
        public string BasePath { get => strBasePath; }
        public List<string> LanguageSetsKeys { get => strLanguageSetsKeys; }
        #endregion
    }
}
