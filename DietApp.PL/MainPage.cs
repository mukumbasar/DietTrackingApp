using DietApp.BL.Managers;
using DietApp.DAL.Concrete;
using DietApp.DAL.Context;
using DietApp.Entities.Common;
using DietApp.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace DietApp.PL
{
    public partial class MainPage : Form
    {
        UserManager userManager;
        MealTypeManager mealTypeManager;
        CategoryManager categoryManager;
        UserFoodManager userFoodManager;
        UserDayMealFoodManager userDayMealFoodManager;
        UserDetailsManager userDetailsManager;
        public MainPage()
        {
            InitializeComponent();
            pnl_LoginPanel.BringToFront();
            userManager = new UserManager(new GenericRepository<User>(new AppDbContext()), new UserRepository(new AppDbContext()));
            userDetailsManager = new UserDetailsManager(new GenericRepository<UserDetails>(new AppDbContext()), new UserDetailsRepository(new AppDbContext()));
            mealTypeManager = new MealTypeManager(new GenericRepository<MealType>(new AppDbContext()));
            categoryManager = new CategoryManager(new GenericRepository<Category>(new AppDbContext()));
            userFoodManager = new UserFoodManager(new GenericRepository<UserFood>(new AppDbContext()), new UserFoodRepository(new AppDbContext()));
            userDayMealFoodManager = new UserDayMealFoodManager(new GenericRepository<UserDayMealFood>(new AppDbContext()), new UserDayMealFoodRepository(new AppDbContext()));
        }

        List<Panel> PanelList;
        private void MainPage_Load(object sender, EventArgs e)
        {
            PanelList = new List<Panel>() { pnl_LoginPanel,pnl_RegisterPage,pnl_ProfilPanel,pnl_MealPanel,pnl_ReportsPanel,pnl_ReportsPanel_EmptyPanel,
                                            pnl_ReportsPanel_DailyCalorieReport,pnl_ReportsPanel_MostyEatedFoodsReport,pnl_ReportsPanel_UserCompareReport,
                                            pnl_ProfilPanel_Info,pnl_ProfilPanel_ChangeInfo};
            pnl_LoginPanel.Enabled = true;

            MealPanel_gb_MealEditGroupBox.Enabled = false;
            MealPanel_btn_UpdateClose.Visible = false;

            LoginPanel_lb_Title.Text = "Kalori Takip Sistemim";

            LoginPanel_lb_Context.Text = "Kalori Takip Sistemi olarak amacımız kullanıcının almış\n" +
                                         "olduğu calorilerinin öğün bazında kayıdını tutmak ve\n" +
                                         "günlük,haftalık ve aylık periyotlarda takibini sağlamaktır.\n" +
                                         "Sağlıklı günler dileriz :)";
            RegisterPanel_lb_Title.Text = LoginPanel_lb_Title.Text;
            RegisterPanel_lb_Context.Text = LoginPanel_lb_Context.Text;


        }

        #region HelperMethods

        /// <summary>
        /// Panel Switch Method
        /// </summary>
        /// <param name="RelatedPanel"></param>
        private void AllPanelsEnableFalseOutsideRelatedPanel(Panel RelatedPanel)
        {
            foreach (var item in PanelList)
            {
                item.Enabled = false;
            }
            RelatedPanel.Enabled = true;
            RelatedPanel.BringToFront();
        }
        /// <summary>
        /// Cleaning and filling Combo Boxes in the Meal Panel
        /// </summary>
        private void RefleshBoxes()
        {
            MealPanel_cb_MealSelection.Items.Clear();
            MealPanel_cb_CatagorySelection.Items.Clear();
            MealPanel_cb_FoodSelection.Items.Clear();

            MealPanel_cb_MealSelection.Text = "Öğün Seçiniz";
            MealPanel_cb_CatagorySelection.Text = "Kategori Seçiniz";
            MealPanel_cb_FoodSelection.Text = "Yemek Seçiniz";
            MealPanel_nup_PortionSelection.Value = 0;

            foreach (var meal in mealTypeManager.GetAll())
            {
                MealPanel_cb_MealSelection.Items.Add(meal.MealName);

            }
            foreach (var category in categoryManager.GetAll())
            {
                MealPanel_cb_CatagorySelection.Items.Add(category.CategoryName);
            }
            foreach (var userFoodName in userFoodManager.GetUserFoods(userManager._id))
            {

                MealPanel_cb_FoodSelection.Items.Add(userFoodName);
            }
        }
        /// <summary>
        /// Closing the Group Box in the Meal Panel and changing the enable feture
        /// </summary>
        private void MealGroupBoxClose()
        {
            MealPanel_gb_MealEditGroupBox.Enabled = false;
            MealPanel_gb_MealEditGroupBox.Visible = false;
            MealPanel_btn_MealAdd.Enabled = true;
            MealPanel_btn_UpdateClose.Visible = false;
        }
        /// <summary>
        /// It creates a struct based on the inputs and transfers the inputs to the Manager to send them to the database for add.
        /// </summary>
        /// <param name="IsGroupBoxClose"></param>
        /// <param name="userIDInput"></param>
        /// <param name="FoodNameInput"></param>
        /// <param name="PortionInput"></param>
        /// <param name="CategoryNameInput"></param>
        /// <param name="CalorieInput"></param>
        /// <param name="MealNameInput"></param>
        /// <param name="dateTimeInput"></param>
        /// <param name="PhotoPathInput"></param>
        private void addMealWithFoodEditGroupBox(string IsGroupBoxClose, int userIDInput, string FoodNameInput, decimal PortionInput, string CategoryNameInput, decimal CalorieInput, string MealNameInput, DateTime dateTimeInput, string PhotoPathInput)
        {
            if (IsGroupBoxClose == "Close")
            {
                if (checkUIValues(MealPanel_cb_MealSelection,
                                   MealPanel_cb_CatagorySelection,
                                   MealPanel_cb_FoodSelection,
                                   MealPanel_nup_PortionSelection,
                                   MealPanel_tb_FoodName,
                                   MealPanel_tb_FoodCalorie,
                                   MealPanel_btn_FoodEdit.Text) && decimal.TryParse(MealPanel_tb_FoodCalorie.Text, out CalorieInput))
                {
                    FoodNameInput = MealPanel_tb_FoodName.Text;
                    PortionInput = MealPanel_nup_PortionSelection.Value;
                    CategoryNameInput = MealPanel_cb_CatagorySelection.Text;
                    //CalorieInput İf'in içinde out ile atanıyor
                    MealNameInput = MealPanel_cb_MealSelection.Text;
                    if (MealPanel_lbl_PhotoPath.Text != null)
                    {
                        PhotoPathInput = MealPanel_lbl_PhotoPath.Text;
                    }


                    StructUserDayMealFood structUserDayMealFood = new StructUserDayMealFood()
                    {
                        UserID = userIDInput,
                        FoodName = FoodNameInput,
                        Portion = PortionInput,
                        CategoryName = CategoryNameInput,
                        Calories = CalorieInput,
                        MealName = MealNameInput,
                        DateTime = dateTimeInput,
                        PhotoPath = PhotoPathInput
                    };
                    MessageBox.Show(userDayMealFoodManager.AddDayMealFood(structUserDayMealFood));
                    MealPanel_Datagrid.DataSource = userDayMealFoodManager.ShowDayMealFoods(userManager._id);
                    RefleshBoxes();
                }
                else
                {
                    MessageBox.Show(" Veri Girişiniz Hatalı");
                }
            }
            else if (IsGroupBoxClose == "Open")
            {
                if (checkUIValues(MealPanel_cb_MealSelection,
                                   MealPanel_cb_CatagorySelection,
                                   MealPanel_cb_FoodSelection,
                                   MealPanel_nup_PortionSelection,
                                   MealPanel_tb_FoodName,
                                   MealPanel_tb_FoodCalorie,
                                   MealPanel_btn_FoodEdit.Text))
                {
                    var UserFood = userFoodManager.GetAll();
                    foreach (var item in UserFood)
                    {
                        if (item.FoodName == MealPanel_cb_FoodSelection.Text)
                        {
                            CalorieInput = item.Calories;
                            break;
                        }
                    }
                    FoodNameInput = MealPanel_cb_FoodSelection.Text;
                    PortionInput = MealPanel_nup_PortionSelection.Value;
                    CategoryNameInput = MealPanel_cb_CatagorySelection.Text;
                    //CalorieInput İf'in içinde out ile atanıyor
                    MealNameInput = MealPanel_cb_MealSelection.Text;
                    if (MealPanel_lbl_PhotoPath.Text != null)
                    {
                        PhotoPathInput = MealPanel_lbl_PhotoPath.Text;
                    }

                    StructUserDayMealFood structUserDayMealFood = new StructUserDayMealFood()
                    {
                        UserID = userIDInput,
                        FoodName = FoodNameInput,
                        Portion = PortionInput,
                        CategoryName = CategoryNameInput,
                        Calories = CalorieInput,
                        MealName = MealNameInput,
                        DateTime = dateTimeInput,
                        PhotoPath = PhotoPathInput
                    };
                    MessageBox.Show(userDayMealFoodManager.AddDayMealFood(structUserDayMealFood));
                    MealPanel_Datagrid.DataSource = userDayMealFoodManager.ShowDayMealFoods(userManager._id);
                    RefleshBoxes();
                }
                else
                {
                    MessageBox.Show(" Veri Girişiniz Hatalı");
                }
            }

        }

        /// <summary>
        /// It creates a struct based on the inputs and transfers the inputs to the Manager to send them to the database for update.
        /// </summary>
        /// <param name="IsGroupBoxClose"></param>
        /// <param name="userDayMealInput"></param>
        /// <param name="userIDInput"></param>
        /// <param name="FoodNameInput"></param>
        /// <param name="PortionInput"></param>
        /// <param name="CategoryNameInput"></param>
        /// <param name="CalorieInput"></param>
        /// <param name="MealNameInput"></param>
        /// <param name="dateTimeInput"></param>
        /// <param name="PhotoPathInput"></param>
        private void updateMealWithFoodEditGroupBox(string IsGroupBoxClose, int userDayMealInput, int userIDInput, string FoodNameInput, decimal PortionInput, string CategoryNameInput, decimal CalorieInput, string MealNameInput, DateTime dateTimeInput, string PhotoPathInput)
        {
            if (IsGroupBoxClose == "Close")
            {
                if (checkUIValues(MealPanel_cb_MealSelection,
                                  MealPanel_cb_CatagorySelection,
                                  MealPanel_cb_FoodSelection,
                                  MealPanel_nup_PortionSelection,
                                  MealPanel_tb_FoodName,
                                  MealPanel_tb_FoodCalorie,
                                  MealPanel_btn_FoodEdit.Text) && decimal.TryParse(MealPanel_tb_FoodCalorie.Text, out CalorieInput))
                {
                    FoodNameInput = MealPanel_tb_FoodName.Text;
                    PortionInput = MealPanel_nup_PortionSelection.Value;
                    CategoryNameInput = MealPanel_cb_CatagorySelection.Text;
                    //CalorieInput İf'in içinde out ile atanıyor
                    MealNameInput = MealPanel_cb_MealSelection.Text;
                    if (MealPanel_lbl_PhotoPath.Text != null)
                    {
                        PhotoPathInput = MealPanel_lbl_PhotoPath.Text;
                    }


                    StructUserDayMealFood structUserDayMealFood = new StructUserDayMealFood()
                    {
                        ID = userDayMealInput,
                        UserID = userIDInput,
                        FoodName = FoodNameInput,
                        Portion = PortionInput,
                        CategoryName = CategoryNameInput,
                        Calories = CalorieInput,
                        MealName = MealNameInput,
                        DateTime = dateTimeInput,
                        PhotoPath = PhotoPathInput
                    };

                    MessageBox.Show(userDayMealFoodManager.UpdateDayMealFood(structUserDayMealFood));
                    MealPanel_Datagrid.DataSource = userDayMealFoodManager.ShowDayMealFoods(userManager._id);
                    RefleshBoxes();
                    MealGroupBoxClose();
                    MessageBox.Show(" Veri Güncellemeniz Başarılı");

                }
                else
                {
                    MessageBox.Show("Veri Güncellemesi İçin İlgili Verinin Bilgilerini\nYukarıda Tekrardan Doldurunuz");
                }
            }
            else if (IsGroupBoxClose == "Open")
            {
                if (checkUIValues(MealPanel_cb_MealSelection,
                                   MealPanel_cb_CatagorySelection,
                                   MealPanel_cb_FoodSelection,
                                   MealPanel_nup_PortionSelection,
                                   MealPanel_tb_FoodName,
                                   MealPanel_tb_FoodCalorie,
                                   MealPanel_btn_FoodEdit.Text))
                {
                    var UserFood = userFoodManager.GetAll();
                    foreach (var item in UserFood)
                    {
                        if (item.FoodName == MealPanel_cb_FoodSelection.Text)
                        {
                            CalorieInput = item.Calories;
                            break;
                        }
                    }

                    FoodNameInput = MealPanel_cb_FoodSelection.Text;
                    PortionInput = MealPanel_nup_PortionSelection.Value;
                    CategoryNameInput = MealPanel_cb_CatagorySelection.Text;
                    //CalorieInput İf'in içinde out ile atanıyor
                    MealNameInput = MealPanel_cb_MealSelection.Text;
                    if (MealPanel_lbl_PhotoPath.Text != null)
                    {
                        PhotoPathInput = MealPanel_lbl_PhotoPath.Text;
                    }


                    StructUserDayMealFood structUserDayMealFood = new StructUserDayMealFood()
                    {
                        ID = userDayMealInput,
                        UserID = userIDInput,
                        FoodName = FoodNameInput,
                        Portion = PortionInput,
                        CategoryName = CategoryNameInput,
                        Calories = CalorieInput,
                        MealName = MealNameInput,
                        DateTime = dateTimeInput,
                        PhotoPath = PhotoPathInput

                    };
                    MessageBox.Show(userDayMealFoodManager.UpdateDayMealFood(structUserDayMealFood));
                    MealPanel_Datagrid.DataSource = userDayMealFoodManager.ShowDayMealFoods(userManager._id);
                    RefleshBoxes();
                    MealGroupBoxClose();
                    MessageBox.Show(" Veri Güncellemeniz Başarılı");

                }
                else
                {
                    MessageBox.Show("Veri Güncellemesi İçin İlgili Verinin Bilgilerini\nYukarıda Tekrardan Doldurunuz");
                }
            }

        }

        /// <summary>
        /// Checking the necessary inputs in Meal Panel for Adding and updateding
        /// </summary>
        /// <param name="MealPanel_cb_MealSelection"></param>
        /// <param name="MealPanel_cb_CatagorySelection"></param>
        /// <param name="MealPanel_cb_FoodSelection"></param>
        /// <param name="MealPanel_nup_PortionSelection"></param>
        /// <param name="MealPanel_tb_FoodName"></param>
        /// <param name="MealPanel_tb_FoodCalorie"></param>
        /// <param name="controlButtonText"></param>
        /// <returns></returns>
        private bool checkUIValues(ComboBox MealPanel_cb_MealSelection,
                                  ComboBox MealPanel_cb_CatagorySelection,
                                  ComboBox MealPanel_cb_FoodSelection,
                                  NumericUpDown MealPanel_nup_PortionSelection,
                                  TextBox MealPanel_tb_FoodName,
                                  TextBox MealPanel_tb_FoodCalorie,
                                  string controlButtonText)
        {
            if (controlButtonText == "+")
            {
                if (MealPanel_cb_MealSelection.SelectedIndex != -1 &&
                    MealPanel_cb_CatagorySelection.SelectedIndex != -1 &&
                    MealPanel_cb_FoodSelection.SelectedIndex != -1 &&
                    MealPanel_nup_PortionSelection.Value != 0)

                { return true; }
                else { return false; }
            }
            else // - ise
            {
                if (MealPanel_tb_FoodName.Text != string.Empty &&
                    MealPanel_tb_FoodCalorie.Text != string.Empty &&
                    MealPanel_cb_MealSelection.SelectedIndex != -1 &&
                    MealPanel_cb_CatagorySelection.SelectedIndex != -1 &&
                    MealPanel_nup_PortionSelection.Value != 0)
                { return true; }
                else { return false; }
            }
        }
        #endregion

        #region Login Panel
        /// <summary>
        /// Logs into systems according to user information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lp_btn_Entry_Click(object sender, EventArgs e)
        {
            userManager.Login(LoginPanel_tb_Email.Text, LoginPanel_tb_Password.Text);
            if (userManager._id != 0)
            {
                ProfilPanel_Info_lbl_Eposta.Text = LoginPanel_tb_Email.Text;
                ProfilPanel_Info_lbl_Password.Text = LoginPanel_tb_Password.Text;

                LoginPanel_tb_Email.Clear();
                LoginPanel_tb_Password.Clear();

                pnl_FlowPanel.Enabled = true;
                pnl_FlowPanel.Visible = true;
                AllPanelsEnableFalseOutsideRelatedPanel(pnl_ProfilPanel);
                pnl_ProfilPanel_Info.BringToFront();
                pnl_ProfilPanel_Info.Enabled = true;
            }
            else
            {
                MessageBox.Show("Giriş Bilgileriniz Hatalı Lütfen Tekrardan Deneyiniz!!!");
            }
        }
        /// <summary>
        /// Switches to the Registration Panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lg_btn_Register_Click(object sender, EventArgs e)
        {
            LoginPanel_tb_Email.Clear();
            LoginPanel_tb_Password.Clear();

            AllPanelsEnableFalseOutsideRelatedPanel(pnl_RegisterPage);
        }
        #endregion

        #region Register Panel

        /// <summary>
        /// Registers the user into the system according to the inputs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rg_btn_Register_Click(object sender, EventArgs e)
        {
            string returnNotification = userManager.AddUser(RegisterPanel_tb_Email.Text, RegisterPanel_tb_Password.Text, RegisterPanel_tb_Password2.Text);
            MessageBox.Show(returnNotification);

            if (returnNotification == "Kullanıcı ekleme başarılı.")
            {
                RegisterPanel_tb_Email.Clear();
                RegisterPanel_tb_Password.Clear();
                RegisterPanel_tb_Password2.Clear();

                AllPanelsEnableFalseOutsideRelatedPanel(pnl_LoginPanel);
            }
        }
        /// <summary>
        /// Switches to the login panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rg_btn_Back_Click(object sender, EventArgs e)
        {
            RegisterPanel_tb_Email.Clear();
            RegisterPanel_tb_Password.Clear();
            RegisterPanel_tb_Password2.Clear();

            AllPanelsEnableFalseOutsideRelatedPanel(pnl_LoginPanel);
        }
        #endregion

        #region Flow Panel
        /// <summary>
        /// Switches to the Profil panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FlowPanel_btn_Profil_Click(object sender, EventArgs e)
        {
            AllPanelsEnableFalseOutsideRelatedPanel(pnl_ProfilPanel);
            pnl_ProfilPanel_Info.BringToFront();
            pnl_ProfilPanel_Info.Enabled = true;
        }
        /// <summary>
        /// Switches to the meal panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FlowPanel_btn_Meals_Click(object sender, EventArgs e)
        {
            MealPanel_Datagrid.DataSource = userDayMealFoodManager.ShowDayMealFoods(userManager._id);
            RefleshBoxes();

            AllPanelsEnableFalseOutsideRelatedPanel(pnl_MealPanel);
        }

        /// <summary>
        /// Switches to the reports panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FlowPanel_btn_Reports_Click(object sender, EventArgs e)
        {
            ReportsPanel_cb_QuerySelection.SelectedIndex = 0;

            AllPanelsEnableFalseOutsideRelatedPanel(pnl_ReportsPanel);
        }

        /// <summary>
        /// Logs out of the system and returns to the login screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mp_btn_Exit_Click(object sender, EventArgs e)
        {
            pnl_FlowPanel.Visible = false;
            pnl_LoginPanel.Enabled = true;

            AllPanelsEnableFalseOutsideRelatedPanel(pnl_LoginPanel);
        }
        #endregion

        #region Profile Panel
        /// <summary>
        /// Switches to Profilepanel Info Subpanel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProfilPanel_Info_btn_EditInfos_Click(object sender, EventArgs e)
        {
            AllPanelsEnableFalseOutsideRelatedPanel(pnl_ProfilPanel);
            pnl_ProfilPanel_ChangeInfo.BringToFront();
            pnl_ProfilPanel_ChangeInfo.Enabled = true;

            ProfilPanel_ChangeInfo_tb_Eposta.Text = ProfilPanel_Info_lbl_Eposta.Text;
            ProfilPanel_ChangeInfo_tb_Password.Text = ProfilPanel_Info_lbl_Password.Text;
            ProfilPanel_ChangeInfo_tb_Name.Text = ProfilPanel_Info_lbl_Name.Text;
            ProfilPanel_ChangeInfo_tb_Surname.Text = ProfilPanel_Info_lbl_Surname.Text;
            ProfilPanel_ChangeInfo_tb_Height.Text = ProfilPanel_Info_lbl_Height.Text;
            ProfilPanel_ChangeInfo_tb_Weight.Text = ProfilPanel_Info_lbl_Weight.Text;
            ProfilPanel_ChangeInfo_tb_Index.Text = ProfilPanel_Info_lbl_Index.Text;
            ProfilPanel_ChangeInfo_pb_ProfileImage.Image = ProfilPanel_Info_pb_ProfileImage.Image;
        }

        /// <summary>
        /// Switches to Profilepanel ChangeInfo Subpanel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProfilPanel_ChangeInfo_btn_Save_Click(object sender, EventArgs e)
        {
            AllPanelsEnableFalseOutsideRelatedPanel(pnl_ProfilPanel);
            pnl_ProfilPanel_Info.BringToFront();
            pnl_ProfilPanel_Info.Enabled = true;

            ProfilPanel_Info_lbl_Eposta.Text = ProfilPanel_ChangeInfo_tb_Eposta.Text;
            ProfilPanel_Info_lbl_Password.Text = ProfilPanel_ChangeInfo_tb_Password.Text;
            ProfilPanel_Info_lbl_Name.Text = ProfilPanel_ChangeInfo_tb_Name.Text;
            ProfilPanel_Info_lbl_Surname.Text = ProfilPanel_ChangeInfo_tb_Surname.Text;
            ProfilPanel_Info_lbl_Height.Text = ProfilPanel_ChangeInfo_tb_Height.Text;
            ProfilPanel_Info_lbl_Weight.Text = ProfilPanel_ChangeInfo_tb_Weight.Text;
            ProfilPanel_Info_lbl_Index.Text = ProfilPanel_ChangeInfo_tb_Index.Text;
            ProfilPanel_Info_pb_ProfileImage.Image = ProfilPanel_ChangeInfo_pb_ProfileImage.Image;

        }
        #endregion

        #region Meal Panel

        /// <summary>
        /// Opens the groupbox for entering meal names and calories.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MealPanel_btn_FoodEdit_Click(object sender, EventArgs e)
        {
            // Panel Animations
            if (!MealPanel_gb_FoodEditGroupBox.Visible) // If meal update is disabled (when - is pressed) (Initial Value "-")
            {
                MealPanel_cb_FoodSelection.Enabled = false;

                MealPanel_btn_FoodEdit.Enabled = true;
                MealPanel_gb_FoodEditGroupBox.Enabled = true;
                MealPanel_gb_FoodEditGroupBox.Visible = true;

                MealPanel_btn_FoodEdit.Text = "-";
            }
            else                                       // If meal update is active (when + is pressed)
            {
                MealPanel_cb_FoodSelection.Enabled = true;

                MealPanel_btn_FoodEdit.Enabled = true;
                MealPanel_gb_FoodEditGroupBox.Visible = false;
                MealPanel_btn_FoodEdit.Text = "+";
            }
        }

        /// <summary>
        /// Adding a Photo to the Meal Panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MealPanel_btn_PhotoAdd_Click(object sender, EventArgs e)
        {
            string PhotoPathInput = string.Empty;
            try
            {
                OpenFileDialog diolog = new OpenFileDialog();
                if (diolog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    PhotoPathInput = diolog.FileName;
                    MealPanel_lbl_PhotoPath.Text = PhotoPathInput;
                    MealPanel_pb_FoodImage.Image = new Bitmap(PhotoPathInput);
                }
            }
            catch
            {
                MealPanel_lbl_PhotoPath.Text = string.Empty;
                MessageBox.Show("Fotoraf Yüklemesi Başarısız.");
            }

        }

        /// <summary>
        /// Adds meals to the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MealPanel_btn_MealAdd_Click(object sender, EventArgs e)
        {
            int userIDInput = userManager._id;
            string FoodNameInput = string.Empty;
            decimal PortionInput = 0;
            string CategoryNameInput = string.Empty;
            decimal CalorieInput = 0;
            string MealNameInput = string.Empty;
            DateTime dateTimeInput = MealPanel_DateTimePicker.Value;
            string PhotoPathInput = string.Empty;
            if (MealPanel_lbl_PhotoPath.Text != null)
            {
                PhotoPathInput = MealPanel_lbl_PhotoPath.Text;
            }


            if (MealPanel_btn_FoodEdit.Text == "-")
            {
                addMealWithFoodEditGroupBox("Close", userIDInput, FoodNameInput, PortionInput, CategoryNameInput, CalorieInput, MealNameInput, dateTimeInput, PhotoPathInput);
            }
            else if (MealPanel_btn_FoodEdit.Text == "+")
            {
                addMealWithFoodEditGroupBox("Open", userIDInput, FoodNameInput, PortionInput, CategoryNameInput, CalorieInput, MealNameInput, dateTimeInput, PhotoPathInput);
            }
        }

        /// <summary>
        /// Updates the Meal in the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MealPanel_btn_MealUpdate_Click(object sender, EventArgs e)
        {
            int userDayMealInput = userDayMealFoodManager.CurrentID;

            int userIDInput = userManager._id;
            string FoodNameInput = string.Empty;
            decimal PortionInput = 0;
            string CategoryNameInput = string.Empty;
            decimal CalorieInput = 0;
            string MealNameInput = string.Empty;
            DateTime dateTimeInput = MealPanel_DateTimePicker.Value;
            string PhotoPathInput = string.Empty;
            if (MealPanel_lbl_PhotoPath.Text != null)
            {
                PhotoPathInput = MealPanel_lbl_PhotoPath.Text;
            }

            if (MealPanel_btn_FoodEdit.Text == "-")
            {
                updateMealWithFoodEditGroupBox("Close", userDayMealInput, userIDInput, FoodNameInput, PortionInput, CategoryNameInput, CalorieInput, MealNameInput, dateTimeInput, PhotoPathInput);
            }
            else if (MealPanel_btn_FoodEdit.Text == "+")
            {
                updateMealWithFoodEditGroupBox("Open", userDayMealInput, userIDInput, FoodNameInput, PortionInput, CategoryNameInput, CalorieInput, MealNameInput, dateTimeInput, PhotoPathInput);
            }
        }

        /// <summary>
        /// Deletes meals from the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MealPanel_btn_MealDelete_Click(object sender, EventArgs e)
        {
            MessageBox.Show(userDayMealFoodManager.DeleteDayMealFood(userDayMealFoodManager.CurrentID));
            MealPanel_Datagrid.DataSource = userDayMealFoodManager.ShowDayMealFoods(userManager._id);
            MealGroupBoxClose();
        }

        /// <summary>
        /// Lists the user's meal history
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MealPanel_btn_ListDataGrid_Click(object sender, EventArgs e)
        {
            MealPanel_Datagrid.DataSource = userDayMealFoodManager.ShowDayMealFoods(userManager._id);
        }

        /// <summary>
        /// Allows selecting meals from data grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MealPanel_Datagrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Select the row to which the selected cell belongs
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = MealPanel_Datagrid.Rows[e.RowIndex];
                selectedRow.Selected = true;
            }

            //Select row for delete and update actions
            if (MealPanel_Datagrid.SelectedRows.Count == 1)
            {
                userDayMealFoodManager.CurrentID = int.Parse(MealPanel_Datagrid.SelectedRows[0].Cells[0].Value.ToString());
                MealPanel_lbl_PhotoPath.Text = MealPanel_Datagrid.SelectedRows[0].Cells[7].Value.ToString();

                string PhotoPathInput = MealPanel_lbl_PhotoPath.Text;
                if (PhotoPathInput == null || PhotoPathInput == string.Empty)
                {
                    MealPanel_pb_FoodImage.Image = Properties.Resources.DefaultFoodImage;
                }
                else
                {
                    try
                    {
                        MealPanel_pb_FoodImage.Image = new Bitmap(PhotoPathInput);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("İlgili öğünün fotoğrafı bilgisyarınızdan\n" +
                                         "Silindiği için eski fotrafı yerine\n" +
                                         "varsayılan fotraf konulmuştur.");

                        MealPanel_pb_FoodImage.Image = Properties.Resources.DefaultFoodImage;
                    }
                }


                MealPanel_gb_MealEditGroupBox.Enabled = true;
                MealPanel_gb_MealEditGroupBox.Visible = true;
                MealPanel_btn_MealAdd.Enabled = false;
                MealPanel_btn_UpdateClose.Visible = true;
            }
            else
            {
                MessageBox.Show("Lütfen Tek Satır Seçiniz");
            }
        }

        /// <summary>
        /// Closes the group box opened for Update and Delete operations.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MealPanel_btn_UpdateClose_Click(object sender, EventArgs e)
        {
            MealGroupBoxClose();
        }

        #endregion

        #region Reports Panel

        /// <summary>
        /// Allows switching from the relevant ComboBox to the desired query panel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReportsPanel_cb_QuerySelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ReportsPanel_cb_QuerySelection.SelectedIndex == 0)
            {
                AllPanelsEnableFalseOutsideRelatedPanel(pnl_ReportsPanel_EmptyPanel);
                pnl_ReportsPanel.Enabled = true;
            }
            else if (ReportsPanel_cb_QuerySelection.SelectedIndex == 1)
            {
                AllPanelsEnableFalseOutsideRelatedPanel(pnl_ReportsPanel_DailyCalorieReport);
                pnl_ReportsPanel.Enabled = true;
            }
            else if (ReportsPanel_cb_QuerySelection.SelectedIndex == 2)
            {
                AllPanelsEnableFalseOutsideRelatedPanel(pnl_ReportsPanel_UserCompareReport);
                pnl_ReportsPanel.Enabled = true;
            }
            else if (ReportsPanel_cb_QuerySelection.SelectedIndex == 3)
            {
                AllPanelsEnableFalseOutsideRelatedPanel(pnl_ReportsPanel_MostyEatedFoodsReport);
                pnl_ReportsPanel.Enabled = true;
            }
        }


        /// <summary>
        /// Lists Daily Meal Report 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReportsPanel_btn_DailyMealCalories_Click(object sender, EventArgs e)
        {
            //Daily Calorie Report with DataGrid
            ReportsPanel_DatagridUserDaily.DataSource = userDayMealFoodManager.ShowDailyMealCalories(userManager._id, ReportsPanel_DateTimePicker.Value);

            List<StructDailyMealCalories> DailyMealList = userDayMealFoodManager.ShowDailyMealCalories(userManager._id, ReportsPanel_DateTimePicker.Value);

            //Total Calorie Calculation
            decimal CaloriesSum = 0;
            foreach (DataGridViewRow row in ReportsPanel_DatagridUserDaily.Rows)
            {
                if (row.Cells["Calories"].Value != null)
                {
                    decimal cellValue;
                    if (decimal.TryParse(row.Cells["Calories"].Value.ToString(), out cellValue))
                    {
                        CaloriesSum += cellValue;
                    }
                }
            }
            ReportsPanel_lbl_TotalDailyCalories.Visible = true;
            ReportsPanel_lbl_TotalDailyCalories.Text = $"{ReportsPanel_DateTimePicker.Value.ToShortDateString()} tarihinde toplam {CaloriesSum} kadar kalori aldınız.";
        }

        /// <summary>
        /// Lists meal reports for the last week or the last month
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReportsPanel_btn_WeekMounthReports_Click(object sender, EventArgs e)
        {
            if (ReportsPanel_rb_MonthllyReport.Checked)
            {
                ReportsPanel_DatagridUserCompare.DataSource = userDayMealFoodManager.ShowReportWeeklyOrMonthlyUserMealCalories(userManager._id, 30);
                ReportsPanel_DatagridOthersCompare.DataSource = userDayMealFoodManager.ShowReportWeeklyOrMonthlyEveryoneMealCalories(userManager._id, 30);
            }
            else if (ReportsPanel_rb_WeeklyReport.Checked)
            {
                ReportsPanel_DatagridUserCompare.DataSource = userDayMealFoodManager.ShowReportWeeklyOrMonthlyUserMealCalories(userManager._id, 7);
                ReportsPanel_DatagridOthersCompare.DataSource = userDayMealFoodManager.ShowReportWeeklyOrMonthlyEveryoneMealCalories(userManager._id, 7);
            }
        }

        /// <summary>
        /// Lists reports about the most eaten foods
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReportsPanel_btn_MostlyEatedReports_Click(object sender, EventArgs e)
        {
            ReportsPanel_DatagridMostyEatedFoodsByFoodName.DataSource = userDayMealFoodManager.ShowReportMostEatenFoodsByFoodName(userManager._id);

            ReportsPanel_DatagridMostyEatedFoodsByMealName.DataSource = userDayMealFoodManager.ShowReportMostEatenFoodsByMeaName(userManager._id);
        }
        #endregion
    }
}
