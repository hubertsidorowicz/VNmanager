using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Media;
using System.Windows;
using System.Text;
using System.Threading.Tasks;
using VNmanager;
using VNmanager.Core;
using Caliburn.Micro;
using System.Windows.Media.Imaging;
using System.Windows.Input;
using System.Data.SQLite;
using Dapper;
using System.Data;
using System.Windows.Controls;
using System.IO;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Win32;
using System.Diagnostics;
using IWshRuntimeLibrary;
using System.Media;

namespace VNmanager
{
    public class MainViewModel : BaseViewModel, IPageViewModel, INotifyPropertyChanged
    {

        #region Private Members
        /// <summary>
        /// Instance for main window
        /// </summary>
        private Window mainWindow { get; set; } = Application.Current.MainWindow;

        /// <summary>
        /// Stores the source for background image
        /// </summary>
        public ImageSource imageSource { get; set; }

        /// <summary>
        /// List of background names
        /// </summary>
        string[] BackgroundNames =
        {
            "AnimeRoom.jpg",
            "Bar.jpg",
            "KyotoDistrict.jpg",
            "SchoolRoof.jpg",
            "TaishoDistrict.jpg",
        };

        /// <summary>
        /// List of shortened month names for converting dates
        /// </summary>
        string[] MonthNames =
        {
            "---",
            "Jan",
            "Feb",
            "Mar",
            "Apr",
            "May",
            "Jun",
            "Jul",
            "Aug",
            "Sep",
            "Oct",
            "Nov",
            "Dec",
        };

        /// <summary>
        /// Stores all added games
        /// </summary>
        private List<GamesModel> _gamesList;

        /// <summary>
        /// Elements of the UI for bindin
        /// </summary>
        private object _sideView;
        private object _menuBarView;

        /// <summary>
        /// Properties of added game
        /// </summary>
        private string _gameTitle;
        private string _titleUrl;
        private string _pageUrl;
        private string _lastPlayed;
        private string _lastPlayDisplay;
        private double _xT;
        private double _yT;
        private double _widthT;
        private double _heightT;
        private double _xP;
        private double _yP;
        private double _widthP;
        private double _heightP;

        /// <summary>
        /// Coordinates of images in TuneGame
        /// </summary>
        private double _x1;
        private double _y1;
        private double _width1;
        private double _height1;
        private double _x2;
        private double _y2;
        private double _width2;
        private double _height2;

        /// <summary>
        /// Path to image to arrow back it changes based on accesibility
        /// </summary>
        private string _backImage;
        /// <summary>
        /// Path to image to arrow forward it changes based on accesibility
        /// </summary>
        private string _forwardImage;

        /// <summary>
        /// Varirable of the view Model binded to Main Window
        /// </summary>
        private IPageViewModel _currentPageViewModel;

        #endregion

        #region Public Members

        /// <summary>
        /// Game currently displayed
        /// </summary>
        public string gameOnDisplay = "";

        /// <summary>
        /// Varirable of the view Model binded to Main Window
        /// </summary>
        public IPageViewModel CurrentPageViewModel
        {
            get
            {
                return _currentPageViewModel;
            }
            set
            {
                _currentPageViewModel = value;
                OnPropertyChanged("CurrentPageViewModel");
            }
        }

        /// <summary>
        /// Side Panel View Model
        /// </summary>
        public SidePanelViewModel SidePanelVM { get; set; }

        /// <summary>
        /// Binding for SideView
        /// </summary>
        public object SideView
        {
            get { return _sideView; }
            set
            {
                _sideView = value;
            }
        }

        /// <summary>
        /// Menu Bar View Model
        /// </summary>
        public MenuBarViewModel MenuBarVM { get; set; }

        /// <summary>
        /// Binding for Menu Bar
        /// </summary>
        public object MenuBarView
        {
            get { return _menuBarView; }
            set
            {
                _menuBarView = value;
            }
        }

        /// <summary>
        /// history of enlighten grids
        /// </summary>
        public Dictionary<string, Grid> grids { get; set; } = new Dictionary<string, Grid>();
        
        /// <summary>
        /// Grid at the begginning 
        /// </summary>
        public Grid allgrid { get; set; }

        /// <summary>
        /// grid currently enlighten
        /// </summary>
        public Grid currentGrid;

        /// <summary>
        /// Path to image to arrow back it changes based on accesibility
        /// </summary>
        public string backImage
        {
            get
            {
                return _backImage;
            }
            set
            {
                _backImage = value;
                OnPropertyChanged("backImage");
            }
        }

        /// <summary>
        /// Path to image to arrow forward it changes based on accesibility
        /// </summary>
        public string forwardImage
        {
            get
            {
                return _forwardImage;
            }
            set
            {
                _forwardImage = value;
                OnPropertyChanged("forwardImage");
            }
        }

        /// <summary>
        /// A transfer info for title image
        /// </summary>
        public double X1
        {
            get
            {
                return _x1;
            }
            set
            {
                _x1 = value;
                OnPropertyChanged("X1");
            }
        }

        /// <summary>
        /// A transfer info for title image
        /// </summary>
        public double Y1
        {
            get
            {
                return _y1;
            }
            set
            {
                _y1 = value;
                OnPropertyChanged("Y1");
            }
        }

        /// <summary>
        /// Width to tune title image
        /// </summary>
        public double Width1
        {
            get
            {
                return _width1;
            }
            set
            {
                _width1 = value;
                OnPropertyChanged("Width1");
            }
        }

        /// <summary>
        /// height to tune title image
        /// </summary>
        public double Height1
        {
            get
            {
                return _height1;
            }
            set
            {
                _height1 = value;
                OnPropertyChanged("Height1");
            }
        }

        /// <summary>
        /// A transfer info for page image
        /// </summary>
        public double X2
        {
            get
            {
                return _x2;
            }
            set
            {
                _x2 = value;
                OnPropertyChanged("X2");
            }
        }

        /// <summary>
        /// A transfer info for page image
        /// </summary>
        public double Y2
        {
            get
            {
                return _y2;
            }
            set
            {
                _y2 = value;
                OnPropertyChanged("Y2");
            }
        }

        /// <summary>
        /// Width to tune page image
        /// </summary>
        public double Width2
        {
            get
            {
                return _width2;
            }
            set
            {
                _width2 = value;
                OnPropertyChanged("Width2");
            }
        }

        /// <summary>
        /// height to tune page image
        /// </summary>
        public double Height2
        {
            get
            {
                return _height2;
            }
            set
            {
                _height2 = value;
                OnPropertyChanged("Height2");
            }
        }

        #endregion

        #region Public Commands

        /// <summary>
        /// Opens window to adding games
        /// </summary>
        public RelayCommand OpenAddWindow { get { return new RelayCommand(OpenAWindow); } }

        /// <summary>
        /// Get from database all records you need to display game
        /// </summary>
        public RelayCommand SetUpProfile { get { return new RelayCommand(SetProfileData); } }

        /// <summary>
        /// Closing app
        /// </summary>
        public RelayCommand CloseApplication { get { return new RelayCommand(Close); } }

        /// <summary>
        /// Minimizing main window
        /// </summary>
        public RelayCommand MinimizeMainWindow { get { return new RelayCommand(Minimize); } }

        /// <summary>
        /// Move back to previous page
        /// </summary>
        public RelayCommand MoveBack { get { return new RelayCommand(App.Nvm.Back); } }
        
        /// <summary>
        /// Move forward to next page
        /// </summary>
        public RelayCommand MoveForward { get { return new RelayCommand(App.Nvm.Forward); } }
        
        /// <summary>
        /// Making game shortcut
        /// </summary>
        public RelayCommand MakeGameShortcut { get { return new RelayCommand(MakeShortcut); } }
        
        /// <summary>
        /// Opening game files
        /// </summary>
        public RelayCommand ViewGameFiles { get { return new RelayCommand(ViewFiles); } }
        
        /// <summary>
        /// Opening tune mode
        /// </summary>
        public RelayCommand DefaultSettings { get { return new RelayCommand(DefTune); } }

        /// <summary>
        /// Changing title width in tune mode
        /// </summary>
        public RelayCommand<string> ChangeTitleWidth { get { return new RelayCommand<string>(TitleWidth); } }

        /// <summary>
        /// Changing title height in tune mode
        /// </summary>
        public RelayCommand<string> ChangeTitleHeight { get { return new RelayCommand<string>(TitleHeight); } }

        /// <summary>
        /// Changing page width in tune mode
        /// </summary>
        public RelayCommand<string> ChangePageWidth { get { return new RelayCommand<string>(PageWidth); } }

        /// <summary>
        /// Changing page height in tune mode
        /// </summary>
        public RelayCommand<string> ChangePageHeight { get { return new RelayCommand<string>(PageHeight); } }
        
        /// <summary>
        /// Changing properties of the game in database
        /// </summary>
        public RelayCommand<string> ChangeGameData { get { return new RelayCommand<string>(ChangeGame); } }
       
        /// <summary>
        /// Removing images from game profile
        /// </summary>
        public RelayCommand<string> DeleteGameImages { get { return new RelayCommand<string>(DeleteImage); } }
        
        /// <summary>
        /// Changing game title
        /// </summary>
        public RelayCommand<string> ChangeGameTitle { get { return new RelayCommand<string>(ChangeTitle); } }
        
        /// <summary>
        /// Delete game data from database
        /// </summary>
        public RelayCommand<string> DeleteGameData { get { return new RelayCommand<string>(DeleteGame); } }
        
        /// <summary>
        /// Change game sorting/arrengament
        /// </summary>
        public RelayCommand<string> ChangeGamesSorting { get { return new RelayCommand<string>(ChangeSorting); } }
        
        /// <summary>
        /// turn on the game
        /// </summary>
        public RelayCommand<string> PlayGame { get { return new RelayCommand<string>(ActivateGame); } }
        
        /// <summary>
        /// Add new game
        /// </summary>
        public RelayCommand<string> AddGameCommand { get { return new RelayCommand<string>(AddNewGame); } }
        
        /// <summary>
        /// Change color of tiles
        /// </summary>
        public RelayCommand<string> ChangeColor { get { return new RelayCommand<string>(Col); } }

        #endregion

        #region Variables

        #region Game Profile Data

        /// <summary>
        /// Stores all added games
        /// </summary>
        public List<GamesModel> gamesList
        {
            get
            {
                return _gamesList;
            }
            set
            {
                _gamesList = value;
                OnPropertyChanged("gamesList");
            }
        }

        /// <summary>
        /// Title of the game
        /// </summary>
        public string gameTitle 
        {
            get { return _gameTitle; }
            set
            {
                _gameTitle = value;
                OnPropertyChanged("gameTitle");
            } 
        }

        /// <summary>
        /// Path to game exe
        /// </summary>
        public string gameUrl { get; set; }

        /// <summary>
        /// Path to title image
        /// </summary>
        public string TitleUrl
        {
            get
            {
                return _titleUrl;
            }
            set
            {
                _titleUrl = value;
                OnPropertyChanged("TitleUrl");
            } 
        }
        
        /// <summary>
        /// Path to page image
        /// </summary>
        public string PageUrl 
        {
            get
            {
                return _pageUrl;
            }
            set
            {
                _pageUrl = value;
                OnPropertyChanged("PageUrl");
            }
        }

        /// <summary>
        /// Date of adding the game
        /// </summary>
        public string TimePlayed { get; set; }

        /// <summary>
        /// Last played date
        /// </summary>
        public string LastPlayed
        {
            get
            {
                return _lastPlayed;
            }
            set
            {
                _lastPlayed = value;
                OnPropertyChanged("LastPlayed");
            }
        }

        /// <summary>
        /// Converted last played date
        /// </summary>
        public string LastPlayDisplay
        {
            get
            {
                return _lastPlayDisplay;
            }
            set
            {
                _lastPlayDisplay = value;
                OnPropertyChanged("LastPlayDisplay");
            }
        }
        
        /// <summary>
        /// Coordinates for title
        /// </summary>
        public double XT
        {
            get
            {
                return _xT;
            }
            set
            {
                _xT = value;
                OnPropertyChanged("XT");
            }
        }

        /// <summary>
        /// Coordinates for title
        /// </summary>
        public double YT
        {
            get
            {
                return _yT;
            }
            set
            {
                _yT = value;
                OnPropertyChanged("YT");
            }
        }

        /// <summary>
        /// Title width
        /// </summary>
        public double WidthT
        {
            get
            {
                return _widthT;
            }
            set
            {
                _widthT = value;
                OnPropertyChanged("WidthT");
            }
        }

        /// <summary>
        /// Title height
        /// </summary>
        public double HeightT
        {
            get
            {
                return _heightT;
            }
            set
            {
                _heightT = value;
                OnPropertyChanged("HeightT");
            }
        }

        /// <summary>
        /// Coordinates for page
        /// </summary>
        public double XP
        {
            get
            {
                return _xP;
            }
            set
            {
                _xP = value;
                OnPropertyChanged("XP");
            }
        }

        /// <summary>
        /// Coordinates for page
        /// </summary>
        public double YP
        {
            get
            {
                return _yP;
            }
            set
            {
                _yP = value;
                OnPropertyChanged("YP");
            }
        }

        /// <summary>
        /// Page width
        /// </summary>
        public double WidthP
        {
            get
            {
                return _widthP;
            }
            set
            {
                _widthP = value;
                OnPropertyChanged("WidthP");
            }
        }

        /// <summary>
        /// Page height
        /// </summary>
        public double HeightP
        {
            get
            {
                return _heightP;
            }
            set
            {
                _heightP = value;
                OnPropertyChanged("HeightP");
            }
        }

        #endregion

        #endregion

        #region Contstructor
        public MainViewModel()
        {
            #region Window/UserControl variables

            ///<summary>
            ///Managing all User Controls and windows
            ///All variables you can access from diff windows/controls
            /// </summary>

            //Side Panel View
            SidePanelVM = new SidePanelViewModel();
            SideView = SidePanelVM;

            //Menu Bar View
            MenuBarVM = new MenuBarViewModel();
            MenuBarView = MenuBarVM;

            CurrentPageViewModel = new GameCollectionViewModel();
            #endregion

            #region Game Collection background settings
            ///<summary>
            ///Drawing Background for game collection and setting imageSource
            /// </summary>

            // instance for random
            Random rnd = new Random();
            // random number
            int imgNum = rnd.Next(0, 4);
            //setting image source for random picture
            imageSource = new BitmapImage(new Uri($"C:/Users/huber/source/repos/VNmanager/VNmanager/Images/Backgrounds/{BackgroundNames[imgNum]}", UriKind.Relative));

            #endregion

            #region Database Operations
            ///<summary>
            ///Load the game collection
            /// </summary>

            try
            {
                LoadGameList();
            }
            catch { }

            #endregion

            #region Tune Settings

            XT = 0;
            YT = 0;
            WidthT = 1;
            HeightT = 1;

            XP = 0;
            YP = 0;
            WidthP = 1;
            HeightP = 1;

            #endregion
        }
        
        #endregion

        #region Command Methods

        #region Sorting Games
        /// <summary>
        /// Sorting games by sorting parameters
        /// </summary>
        
        public string sortingKey = "none";
        public List<GamesModel> SortGamesList(List<GamesModel> list)
        {
            if(sortingKey == "Alphabetical")
            {
                IEnumerable<GamesModel> listByTitle = list.OrderBy(p => p.Title);
                return listByTitle.ToList();
            }
            else if(sortingKey == "Last Added")
            {
                list.Reverse();
                return list;
            }
            return list;
        }

        public void ChangeSorting(string key)
        {

            sortingKey = key;
            RefreshPage();
        }

        #endregion

        #region Game Profile options
        /// <summary>
        /// Functions operating commands from game profile
        /// </summary>
        /// <param name="url"></param>
        public void ActivateGame(string url)
        {
            #region Update Last Played

            LastPlayed = DateTime.Now.ToString();
            var game = SqliteDataAccess.LoadOneGame(gameTitle);
            game[0].LastPlayed = LastPlayed;
            SqliteDataAccess.UpdateGame(game[0], gameTitle, "LastPlayed");
            #endregion

            #region Turn on the game

            ProcessStartInfo pInfo = new ProcessStartInfo(gameUrl);
            try
            {
                Process p = Process.Start(pInfo);
            }
            catch { }

            #endregion
        }

        public void MakeShortcut(object o)
        {
            object shDesktop = (object)"Desktop";
            WshShell shell = new WshShell();
            string shortcutAddress = (string)shell.SpecialFolders.Item(ref shDesktop) + @"\"+gameTitle+".lnk";
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutAddress);
            shortcut.Description = "Shortcut for"+gameTitle;
            shortcut.Hotkey = "Ctrl+Shift+N";
            shortcut.TargetPath = gameUrl;
            shortcut.Save();
        }
        public void ViewFiles(object o)
        {
            string temp = Path.GetDirectoryName(gameUrl);
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
            {
                FileName = temp,
                UseShellExecute = true,
                Verb = "open"
            });
        }


        #endregion

        #region Database functions
        /// <summary>
        /// Adding, removing, changing games' parameters
        /// Queries to database
        /// </summary>

        public void LoadGameList()
        {
            RefreshPage();
        }

        public string title;
        public void AddNewGame(string pickedGame)
        {
            if (!CheckGame(pickedGame)) return;
            

            GamesModel gm = new GamesModel();
            gm.Id = 0;
            gm.Title = title;
            gm.GameUrl = gameUrl;
            gm.LastPlayed = "";
            gm.Added = DateTime.Today.ToString();
            gm.Icon = IconToBytes(IconFromFilePath(gameUrl));
            gm.TitleImage = "";
            gm.PageImage = "";
            gm.XT = 0;
            gm.YT = 0;
            gm.WidthT = 1;
            gm.HeightT = 1;
            gm.XP = 0;
            gm.YP = 0;
            gm.WidthP = 1;
            gm.HeightP = 1;
            SqliteDataAccess.SaveGames(gm);

            RefreshPage();
        }
        
        private void ChangeTitle(string itemToEdit)
        {
            GetTitle w = new GetTitle();
            w.ShowDialog();
        }

        public string newTitle = "Valorant";
        private void ChangeGame(string itemToEdit)
        {
            var game = SqliteDataAccess.LoadOneGame(gameOnDisplay);


            if (itemToEdit == "Title") 
            {
                if (newTitle == "" || newTitle.Replace(" ","").Length == 0)
                {
                    //........................................
                }
                else
                {
                    game[0].Title = newTitle;
                    SqliteDataAccess.UpdateGame(game[0], gameOnDisplay, itemToEdit);
                    RefreshPage();
                    gameTitle = newTitle;
                    gameOnDisplay = newTitle;
                }
            
            }
            else if(itemToEdit == "PageImage")
            {
                // make all security stuff ! ! ! ! ! !
                OpenFileDialog fileDialog = new OpenFileDialog();
                if (fileDialog.ShowDialog() == true)
                {
                    game[0].PageImage = fileDialog.FileName;
                }
                else
                {
                    // error message
                }

                SqliteDataAccess.UpdateGame(game[0], gameOnDisplay, itemToEdit);
                RefreshPage();
                PageUrl = game[0].PageImage;
            }
            else if(itemToEdit == "TitleImage")
            {
                // make all security stuff ! ! ! ! ! !
                OpenFileDialog fileDialog = new OpenFileDialog();
                if (fileDialog.ShowDialog() == true)
                {
                    game[0].TitleImage = fileDialog.FileName;
                }
                else
                {
                    // error message
                }
                SqliteDataAccess.UpdateGame(game[0], gameOnDisplay, itemToEdit);
                RefreshPage();
                TitleUrl = game[0].TitleImage;
            }
            else if(itemToEdit == "Sizes")
            {
                game[0].XT = X1;
                game[0].YT = Y1;
                game[0].WidthT = Width1;
                game[0].HeightT = Height1;

                game[0].XP = X2;
                game[0].YP = Y2;
                game[0].WidthP = Width2;
                game[0].HeightP = Height2;
                
                SqliteDataAccess.UpdateGame(game[0], gameOnDisplay, "XT");
                SqliteDataAccess.UpdateGame(game[0], gameOnDisplay, "YT");
                SqliteDataAccess.UpdateGame(game[0], gameOnDisplay, "WidthT");
                SqliteDataAccess.UpdateGame(game[0], gameOnDisplay, "HeightT");     
                
                SqliteDataAccess.UpdateGame(game[0], gameOnDisplay, "XP");
                SqliteDataAccess.UpdateGame(game[0], gameOnDisplay, "YP");
                SqliteDataAccess.UpdateGame(game[0], gameOnDisplay, "WidthP");
                SqliteDataAccess.UpdateGame(game[0], gameOnDisplay, "HeightP");
                
                RefreshPage();

                XT = X1;
                YT = X1;
                WidthT = Width1;
                HeightT = Height1;

                XP = X2;
                YP = X2;
                WidthP = Width2;
                HeightP = Height2;
            }

        }

        private void DeleteImage(string itemToEdit)
        {
            var game = SqliteDataAccess.LoadOneGame(gameOnDisplay);
            if (itemToEdit == "TitleImage")
            {
                game[0].TitleImage = "";
                SqliteDataAccess.UpdateGame(game[0], gameOnDisplay, itemToEdit);
                TitleUrl = "";
                RefreshPage();
            }
            else if(itemToEdit == "PageImage")
            {
                game[0].PageImage = "";
                SqliteDataAccess.UpdateGame(game[0], gameOnDisplay, itemToEdit);
                PageUrl = "";
                RefreshPage();
            }
        }

        public void DeleteGame(string gameTitle)
        {
            SqliteDataAccess.DeleteGame(gameTitle);
            RefreshPage();
        }

        private bool isGameInCollection(string gameTitle)
        {
            var check = SqliteDataAccess.LoadOneGame(gameTitle);

            if (check.Count == 0 && gameTitle != "Collection")
                return false;

            return true;
        }

        #endregion

        #endregion

        #region Helpers

        private void OpenAWindow(object o)
        {
            var addMenu = new AddWindow();
            addMenu.ShowDialog();
        }
        private void Close(object o)
        {
            System.Windows.Application.Current.Shutdown();
        }
        private void Minimize(object o)
        {
            mainWindow.WindowState = WindowState.Minimized;
        }

        private string ConvertDate(string date, string sign)
        {
            if (date == null || date == "")
                return "Not played yet :)";

            string day = date.Substring(0, 2);
            string month = date.Substring(3, 2);

            if (day[0] == '0')
                day = day[1] + "";

            if (month[0] == '0')
                month = month[1] + "";

            int k = System.Int32.Parse(month);
            string n = sign + day + " " + MonthNames[k];

            return n;
        }

        private bool CheckGame(string pickedGame)
        {
            if (pickedGame == null)
                return false;

            if (Path.GetExtension(pickedGame) != ".exe")
            {
                InvalidWindow popup = new InvalidWindow();
                SystemSounds.Asterisk.Play();
                popup.ShowDialog();
                return false;
            }

            if (App.Nvm.isGameInCollection(title) == true)
                return false;

            return true;
        }
        public void SetProfileData(object o)
        {
            if (gameOnDisplay == "Collection")
                return;

            var list = SqliteDataAccess.LoadOneGame(gameOnDisplay);

            gameTitle = list[0].Title;
            gameUrl = list[0].GameUrl;
            TitleUrl = list[0].TitleImage;
            PageUrl = list[0].PageImage;
            LastPlayed = list[0].LastPlayed;
            LastPlayDisplay = ConvertDate(LastPlayed,"Last Played: ");
            TimePlayed = ConvertDate(list[0].Added, "Added: ");
            XT = list[0].XT;
            YT = list[0].YT;
            WidthT = list[0].WidthT;
            HeightT = list[0].HeightT;
            XP = list[0].XP;
            YP = list[0].YP;
            WidthP = list[0].WidthP;
            HeightP = list[0].HeightP;
        }

        public void RefreshPage()
        {
            gamesList = SortGamesList(SqliteDataAccess.LoadGames());
            grids.Clear();
        }

        private void TitleWidth(string s)
        {
            if (s == "small" && Width1 != 0)
            {
                Width1 -= 0.1;
            }

            if (s == "big" && Width1 != 10)
            {
                Width1 += 0.1;
            }
        }

        private void TitleHeight(string s)
        {
            if (s == "small" && Height1 != 0)
            {
                Height1 -= 0.1;
            }

            if (s == "big" && Height1 != 10)
            {
                Height1 += 0.1;
            }
        }

        private void PageWidth(string s)
        {
            if (s == "small" && Width2 != 0)
            {
                Width2 -= 0.1;
            }

            if (s == "big" && Width2 != 10)
            {
                Width2 += 0.1;
            }
        }

        private void PageHeight(string s)
        {
            if (s == "small" && Height2 != 0)
            {
                Height2 -= 0.1;
            }

            if (s == "big" && Height2 != 10)
            {
                Height2 += 0.1;
            }
        }
        private void DefTune(object s)
        {
            X1 = 0;
            Y1 = 0;

            Width1 = 1.0;
            Height1 = 1.0;

            X2 = 0;
            Y2 = 0;

            Width2 = 1.0;
            Height2 = 1.0;
        }
        #region Icon Functions

        /// <summary>
        /// Retrieve Icon of the exe file
        /// </summary>
        /// <param name="gameUrl"></param>
        /// <returns></returns>
        private static Icon IconFromFilePath(string gameUrl)
        {
            var result = (Icon)null;

            try
            {
                result = Icon.ExtractAssociatedIcon(gameUrl);
            }
            catch (System.Exception)
            {
                // default icon
            }
            return result;
        }

        /// <summary>
        /// Change System.Drawing.Icon to ByteArray
        /// </summary>
        /// <param name="icon"></param>
        /// <returns></returns>
        private static byte[] IconToBytes(Icon icon)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                icon.Save(ms);
                return ms.ToArray();
            }
        }

        private void Col(string title)
        {
            var grid = grids.FirstOrDefault(x => x.Key == title).Value;

            if (title == "Collection")
                grid = allgrid;

            var bc = new BrushConverter();

            if (currentGrid != null)
            {
                currentGrid.Background = (System.Windows.Media.Brush)bc.ConvertFrom("#272537");
            }

            grid.Background = (System.Windows.Media.Brush)bc.ConvertFrom("#565277");
            currentGrid = grid;
        }

        #endregion

        #endregion

    }
}