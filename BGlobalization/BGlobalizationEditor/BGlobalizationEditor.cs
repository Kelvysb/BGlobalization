using BGlobalization;
using BGlobalization.Basic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BGlobalizationEditor
{
    public partial class BGlobalizationEditor : Form
    {

        #region Declarations
        BindingSource objBindingGrid;
        BindingSource objBindingList;
        bool blnLoaded;
        #endregion

        #region Constructor
        public BGlobalizationEditor()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Events
        private void BGlobalizationEditor_Load(object sender, EventArgs e)
        {
            try
            {
                LoadPage();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                SaveSet();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbKeys_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SelectItem();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region functions
        private void LoadPage()
        {
            try
            {
                blnLoaded = false;

                objBindingGrid = new BindingSource();
                objBindingList = new BindingSource();

                cmbKeys.DataSource = objBindingList;
                grdGlobalization.DataSource = objBindingGrid;

                blnLoaded = true;

            }
            catch (Exception)
            {
                throw;
            }
        }

        private void Open()
        {

            FolderBrowserDialog objDialog;

            try
            {
                blnLoaded = false;

                objDialog = new FolderBrowserDialog();
                objDialog.Description = "Select Folder.";

                if (objDialog.ShowDialog() == DialogResult.OK)
                {
                    BGLanguage.Initialize(objDialog.SelectedPath);
                    objBindingList.DataSource = BGLanguage.Instance.LanguageSetsKeys;
                    blnLoaded = true;
                    if (BGLanguage.Instance.LanguageSetsKeys.Count > 0)
                    {
                        cmbKeys.SelectedIndex = 0;
                        SelectItem();
                    }

                }



            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                blnLoaded = true;
            }
        }

        private void Save()
        {
            try
            {
                BGLanguage.Instance.Save();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void SaveSet()
        {
            BGLanguageSet objAuxLanguageSet;

            try
            {
                blnLoaded = false;

                objAuxLanguageSet = BGLanguage.Instance.LanguageSets.Find(item => { return item.LanguageKey.Equals(txtKey.Text, StringComparison.InvariantCultureIgnoreCase); });

                if (objAuxLanguageSet == null)
                {
                    objAuxLanguageSet = new BGLanguageSet();
                    objAuxLanguageSet.LanguageKey = txtKey.Text;
                    objAuxLanguageSet.Description = txtDescription.Text;
                    objAuxLanguageSet.Default = chkDefault.Checked;
                    BGLanguage.Instance.AddLanguageSet(objAuxLanguageSet);
                }
                else
                {
                    objAuxLanguageSet.Description = txtDescription.Text;
                    objAuxLanguageSet.Default = chkDefault.Checked;
                }

                objBindingList.ResetBindings(true);

                blnLoaded = true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                blnLoaded = true;
            }
        }

        private void SelectItem()
        {

            BGLanguageSet objAuxLanguageSet;

            try
            {
                if (blnLoaded)
                {
                    objAuxLanguageSet = BGLanguage.Instance.LanguageSets.Find(item => { return item.LanguageKey.Equals(cmbKeys.SelectedValue.ToString(), StringComparison.InvariantCultureIgnoreCase); });
                    objBindingGrid.DataSource = objAuxLanguageSet.Itens;
                    txtKey.Text = objAuxLanguageSet.LanguageKey;
                    txtDescription.Text = objAuxLanguageSet.Description;
                    chkDefault.Checked = objAuxLanguageSet.Default;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion


    }
}
