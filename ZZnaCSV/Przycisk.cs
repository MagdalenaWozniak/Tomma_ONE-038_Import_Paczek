using Hydra;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

[assembly: CallbackAssemblyDescription(Name: "ONE-038 Import Paczek",
                                           Description: "Excel -> Paczka",
                                           Author: "OneSupport",
                                           Version: "1.0",
                                           SystemVersion: "2023.2.2",
                                           Created: "25-01-2024")]
namespace Przycisk
{
    [SubscribeProcedure(Procedures.Preliminarz, "Excel -> Paczka")]
    public class DodajFakture : Callback
    {
        private ClaWindow RapButton;
        private ClaWindow zakladkaTabWewnetrzne;
        private ClaWindow filtrBtn;
        public override void Init()
        {
            AddSubscription(true, 0, Events.OpenWindow, new TakeEventDelegate(OnOpenWindow));
            AddSubscription(false, 0, Events.ResizeWindow, new TakeEventDelegate(Resize));
        }



        private bool OnOpenWindow(Procedures ProcId, int ControlId, Events Event)
        {

            ClaWindow Parent = GetWindow();
            Rectangle WndBnd = GetWindow().Bounds;
            zakladkaTabWewnetrzne = GetWindow().AllChildren["?PreliminarzTab"];
            RapButton = zakladkaTabWewnetrzne.Children.Add(ControlTypes.button);
            RapButton.Visible = true;
            RapButton.TextRaw = "Paczki";
            RapButton.ToolTipRaw = "Dodaj dokumenty do paczki przelewów";
            RapButton.OnAfterMouseDown += new TakeEventDelegate(BtnOnMouseDown);

            filtrBtn = GetWindow().AllChildren["?Insert"];                            //pobiera współrzędne przycisku zapisz
            RapButton.Bounds = new Rectangle(filtrBtn.Bounds.X - 25, filtrBtn.Bounds.Y, 22, 15);
            //listID = GetWindow().AllChildren["?PreliminarzBrowse"].Id;      //tu sprawdzic

            //pobrane z pola w zakładce Raporty
            return true;
        }

        public bool Resize(Procedures ProcId, int ControlId, Events Event)
        {
            filtrBtn = GetWindow().AllChildren["?Insert"];                            //pobiera współrzędne przycisku zapisz
            RapButton.Bounds = new Rectangle(filtrBtn.Bounds.X - 25, filtrBtn.Bounds.Y, 22, 15);
            return true;
        }

        private bool BtnOnMouseDown(Procedures ProcId, int ControlId, Events Event)
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //MessageBox.Show(Runtime.ConfigurationDictionary.Baza.ToString());
            if (Runtime.ConfigurationDictionary.Baza.ToString().Substring(0, 20) == "ERPXL_Tomma_test_NEW")
            {
                //MessageBox.Show("Po wybraniu pliku nastąpi dodawanie danych do bazy ERPXL_TOMMA-ERP", "Komunikat",
                //MessageBoxButtons.OK, MessageBoxIcon.Information);
                Process.Start("C:\\OneSupport\\ImportPaczek\\TestNEW\\ImportPaczek.exe");
            }
            else if (Runtime.ConfigurationDictionary.Baza.ToString().Substring(0, 16) == "ERPXL_Tomma_test")
            {
                //MessageBox.Show("Po wybraniu pliku nastąpi dodawanie danych do bazy ERPXL_TOMMA_test", "Komunikat",
                // MessageBoxButtons.OK, MessageBoxIcon.Information);
                Process.Start("C:\\OneSupport\\ImportPaczek\\Test\\ImportPaczek.exe");
            }
            else if (Runtime.ConfigurationDictionary.Baza.ToString().Substring(0, 15) == "ERPXL_TOMMA-ERP")
            {
                //MessageBox.Show("Po wybraniu pliku nastąpi dodawanie danych do bazy ERPXL_TOMMA-ERP", "Komunikat",
                 //MessageBoxButtons.OK, MessageBoxIcon.Information);
                Process.Start("C:\\OneSupport\\ImportPaczek\\Produkcja\\ImportPaczek.exe");
            }
            else if (Runtime.ConfigurationDictionary.Baza.ToString().Substring(0, 18) == "ERPXL_BONUS_2_test")
            {
                // MessageBox.Show("Po wybraniu pliku nastąpi dodawanie danych do bazy ERPXL_TOMMA-ERP", "Komunikat",
                // MessageBoxButtons.OK, MessageBoxIcon.Information);
                Process.Start("C:\\OneSupport\\ImportPaczek\\Bonus_test\\ImportPaczek.exe");
            }
            else if (Runtime.ConfigurationDictionary.Baza.ToString().Substring(0, 13) == "ERPXL_BONUS_2")
            {
                // MessageBox.Show("Po wybraniu pliku nastąpi dodawanie danych do bazy ERPXL_TOMMA-ERP", "Komunikat",
                // MessageBoxButtons.OK, MessageBoxIcon.Information);
                Process.Start("C:\\OneSupport\\ImportPaczek\\Bonus_Produkcja\\ImportPaczek.exe");
            }

            else
            {
                MessageBox.Show("Dla bazy " + Runtime.ConfigurationDictionary.Baza.ToString() + " dodatek nie posiada funkcjonalności", "Komunikat",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


            return true;
        }
        #region
        public override void Cleanup()
        {
        }
    }
}

#endregion