using System;
using System.Collections.Generic;
using BGlobalization.Exceptions;
using Newtonsoft.Json;

namespace BGlobalization.Basic
{
    public class BGLanguageSet
    {

        #region Constructor
        public BGLanguageSet()
        {
            try
            {
                Itens = new List<BGLanguageItem>();
                Default = false;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Functions
        public string GetValue(string p_strKey)
        {

            BGLanguageItem objAuxReturn;

            try
            {

                objAuxReturn = Itens.Find((BGLanguageItem item) => { return item.Key.Equals(p_strKey, StringComparison.InvariantCultureIgnoreCase); });

                if (objAuxReturn == null)
                {
                    throw new ValueNotFoundException(p_strKey);
                }

                return objAuxReturn.Value;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public string Serialize()
        {
            try
            {
                return JsonConvert.SerializeObject(this, Formatting.Indented);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static BGLanguageSet Deserialize(string p_strJson)
        {
            try
            {
                return JsonConvert.DeserializeObject<BGLanguageSet>(p_strJson);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Properties
        [JsonProperty("LANGUAGEKEY")]
        public string LanguageKey { get; set; }

        [JsonProperty("DESCRIPTION")]
        public string Description { get; set; }

        [JsonProperty("DEFAULT")]
        public bool Default { get; set; }

        [JsonProperty("ITENS")]
        public List<BGLanguageItem> Itens { get; set; }
        #endregion

    }

    public class BGLanguageItem
    {

        #region Properties
        [JsonProperty("KEY")]
        public string Key { get; set; }

        [JsonProperty("VALUE")]
        public string Value { get; set; }

        [JsonProperty("DESCRIPTION")]
        public string Description { get; set; }
        #endregion
    }

}
