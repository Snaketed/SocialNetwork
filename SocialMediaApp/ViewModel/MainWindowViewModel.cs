using Bll.AbstractBll;
using Bll.Concrete;
using Models;
using MvvmSwitchViews;
using SocialMediaApp.Helper;
using SocialMediaApp.View.UserControls;
using SocialMediaApp.View.UserControls.MainUCUserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SocialMediaApp.ViewModel
{
    class MainWindowViewModel : AViewModel
    {
        private readonly IUserBll UserBll = new UserBll();
        private readonly IPostBll PostBll = new PostBll();

        private readonly object SignInUC = new SignInUC();
        private readonly object SignUpUC = new SignUpUC();
        private readonly object MainUC = new MainUC();

        private readonly object EditNicknameUC = new EditNicknameUC();
        private readonly object EditPasswordUC = new EditPasswordUC();
        private readonly object EditProfileInfoUC = new EditProfileInfoUC();

        private readonly object MyPostsUC = new MyPostsUC();
        private readonly object MyProfileUC = new MyProfileUC();
        private readonly object AddPostUC = new AddPostUC();
        private readonly object PostUC = new PostUC();
        private readonly object EmptyUC = new EmptyUC();
        private readonly object FavoriteUsersPostUC = new FavoriteUsersPostUC();
        private readonly object AllPostsUC = new AllPostsUC();
        private readonly object CommentsUC = new CommentsUC();
        private readonly object AddCommentUC = new AddCommentUC();
        private readonly object AddFavoriteUserUC = new AddFavoriteUserUC();
        private readonly object DeleteFavoriteUserUC = new DeleteFavoriteUserUC();
        private readonly object ShowUserInfoUC = new ShowUserInfoUC();
        private readonly object UserProfileUC = new UserProfileUC();
        ///UsersList list Prop
        private List<string> UserNicknames = new List<string>();
        private PersonInfoModel CurrentUserProfilModel = new PersonInfoModel();

        private string _nicknameUser;
        public string NicknameUser
        {
            get { return _nicknameUser; }
            set
            {
                _nicknameUser = value;
                Error = "";
                OnPropertyChanged("NicknameUser");
            }
        }
        public string MailAddressUser
        {
            get { return CurrentUserProfilModel.MailAddress; }
            set
            {
                Error = "";
                CurrentUserProfilModel.MailAddress = value;
                OnPropertyChanged("MailAddressUser");
            }
        }
        public string PhoneNumberUser
        {
            get { return CurrentUserProfilModel.PhoneNumber; }
            set
            {
                Error = "";
                CurrentUserProfilModel.PhoneNumber = value;
                OnPropertyChanged("PhoneNumberUser");
            }
        }
        public string FirstNameUser
        {
            get { return CurrentUserProfilModel.FirstName; }
            set
            {
                Error = "";
                CurrentUserProfilModel.FirstName = value;
                OnPropertyChanged("FirstNameUser");
            }
        }
        public string LastNameUser
        {
            get { return CurrentUserProfilModel.LastName; }
            set
            {
                Error = "";
                CurrentUserProfilModel.LastName = value;
                OnPropertyChanged("LastNameUser");
            }
        }
        public DateTime BornDateUser
        {
            get { return CurrentUserProfilModel.BornDate; }
            set
            {
                Error = "";
                CurrentUserProfilModel.BornDate = value;
                OnPropertyChanged("BornDateUser");
            }
        }

        private string _pageNumerationUserInfo;
        public string PageNumerationUserInfo
        {
            get { return _pageNumerationUserInfo; }
            set
            {
                _pageNumerationUserInfo = value;
                Error = "";
                OnPropertyChanged("PageNumerationUserInfo");
            }
        }

        private int _pageUserInfo = 1;
        public int PageUserInfo
        {
            get { return _pageUserInfo; }
            set
            {
                _pageUserInfo = value;
                Error = "";
                OnPropertyChanged("PageUserInfo");
            }
        }
        ///Comment list Prop
        private List<CommentModel> CommentModels = new List<CommentModel>();
        private CommentModel CurrentCommentModel = new CommentModel();
        private int _commentPage=1;
        public int CommentPage
        {
            get { return _commentPage; }
            set
            {
                _commentPage = value;
                Error = "";
                OnPropertyChanged("CommentPage");
            }
        }

        private string _commentPageNumeration;
        public string CommentPageNumeration
        {
            get { return _commentPageNumeration; }
            set
            {
                _commentPageNumeration = value;
                Error = "";
                OnPropertyChanged("CommentPageNumeration");
            }
        }
        public string CommentText
        {
            get { return CurrentCommentModel.Text; }
            set
            {
                CurrentCommentModel.Text = value;
                Error = "";
                OnPropertyChanged("Text");
            }
        }
        public DateTime CommentDate
        {
            get { return CurrentCommentModel.Date; }
            set
            {
                CurrentCommentModel.Date = value;
                Error = "";
                OnPropertyChanged("PostDate");
            }
        }
        public string CommentOwner
        {
            get { return CurrentCommentModel.CommentOwnerNickname; }
            set
            {
                CurrentCommentModel.CommentOwnerNickname = value;
                Error = "";
                OnPropertyChanged("PostOwnerNickname");
            }
        }


        ///PostList Prop
        private List<PostModel> PostModels = new List<PostModel>();
        private PostModel CurrentPostModel = new PostModel();
        private int _page;
        public int Page
        {
            get { return _page; }
            set
            {
                _page = value;
                Error = "";
                OnPropertyChanged("Page");
            }
        }
        private string _pageNumeration;
        public string PageNumeration
        {
            get { return _pageNumeration; }
            set
            {
                _pageNumeration = value;
                Error = "";
                OnPropertyChanged("PageNumeratiom");
            }
        }
        public string Text
        {
            get { return CurrentPostModel.Text; }
            set
            {
                CurrentPostModel.Text = value;
                Error = "";
                OnPropertyChanged("Text");
            }
        }
        public string PicturePath
        {
            get { return CurrentPostModel.PicturePath; }
            set
            {
                CurrentPostModel.PicturePath = value;
                Error = "";
                OnPropertyChanged("PicturePath");
            }
        }
        public DateTime PostDate
        {
            get { return CurrentPostModel.Date; }
            set
            {
                CurrentPostModel.Date = value;
                Error = "";
                OnPropertyChanged("PostDate");
            }
        }
        public string PostOwnerNickname
        {
            get { return CurrentPostModel.PostOwnerNickname; }
            set
            {
                CurrentPostModel.PostOwnerNickname = value;
                Error = "";
                OnPropertyChanged("PostOwnerNickname");
            }
        }

        private int _numberOfPost;
        public int NumberOfPost
        {
            get { return _numberOfPost; }
            set
            {
                _numberOfPost = value;
                OnPropertyChanged("NumberOfPost");
            }
        }

       

        private int _numberOfDisikes;
        public int NumberOfDislikes
        {
            get { return _numberOfDisikes; }
            set
            {
                _numberOfDisikes = value;
                OnPropertyChanged("NumberOfDislikes");
            }
        }

        private string _filterNickname;
        public string FilterNickname
        {
            get { return _filterNickname; }
            set
            {
                _filterNickname = value;
                OnPropertyChanged("FilterNickname");
            }
        }
        ///UserModel Prop
        public string Nickname
        {
            get { return UserBll.User.Nickname; }
            set
            {
                UserBll.User.Nickname = value;
                Error = "";
                OnPropertyChanged("Nickname");
            }
        }
        public string Password
        {
            get { return UserBll.User.Password; }
            set
            {
                Error = "";
                UserBll.User.Password = value;
                OnPropertyChanged("Password");
            }
        }
        public string MailAddress
        {
            get { return UserBll.User.PersonInfo.MailAddress; }
            set
            {
                Error = "";
                UserBll.User.PersonInfo.MailAddress = value;
                OnPropertyChanged("MailAddress");
            }
        }
        public string PhoneNumber
        {
            get { return UserBll.User.PersonInfo.PhoneNumber; }
            set
            {
                Error = "";
                UserBll.User.PersonInfo.PhoneNumber = value;
                OnPropertyChanged("PhoneNumber");
            }
        }
        public string FirstName
        {
            get { return UserBll.User.PersonInfo.FirstName; }
            set
            {
                Error = "";
                UserBll.User.PersonInfo.FirstName = value;
                OnPropertyChanged("FirstName");
            }
        }
        public string LastName
        {
            get { return UserBll.User.PersonInfo.LastName; }
            set
            {
                Error = "";
                UserBll.User.PersonInfo.LastName = value;
                OnPropertyChanged("LastName");
            }
        }
        public DateTime BornDate
        {
            get { return UserBll.User.PersonInfo.BornDate; }
            set
            {
                Error = "";
                UserBll.User.PersonInfo.BornDate = value;
                OnPropertyChanged("BornDate");
            }
        }

        private int _subscribersNumber;
        public int SubscribersNumber
        {
            get { return _subscribersNumber; }
            set
            {
                _subscribersNumber = value;
                OnPropertyChanged("SubscribersNumber");
            }
        }

        private int _favoriteUsersNumber;
        public int FavoriteUsersNumber
        {
            get { return _favoriteUsersNumber; }
            set
            {
                _favoriteUsersNumber = value;
                OnPropertyChanged("FavoriteUsersNumber");
            }
        }

        private int _numberOfLikes;
        public int NumberOfLikes
        {
            get { return _numberOfLikes; }
            set
            {
                _numberOfLikes = value;
                OnPropertyChanged("NumberOfLikes");
            }
        }

        private string _selectedNickname;
        public string SelectedNickname
        {
            get { return _selectedNickname; }
            set
            {
                Error = "";
                _selectedNickname = value;
                OnPropertyChanged("SelectedNickname");
            }
        }
       
        //EditProp
        private string _nicknameEdit;
        public string NicknameEdit
        {
            get { return _nicknameEdit; }
            set
            {
                _nicknameEdit = value;
                Error = "";
                OnPropertyChanged("NicknameEdit");
            }
        }
        private string _passwordEdit;
        public string PasswordEdit
        {
            get { return _passwordEdit; }
            set
            {
                Error = "";
                _passwordEdit = value;
                OnPropertyChanged("PasswordEdit");
            }
        }
        private PersonInfoModel _personInfoModelEdit=new PersonInfoModel();
        public string MailAddressEdit
        {
            get { return _personInfoModelEdit.MailAddress; }
            set
            {
                Error = "";
                _personInfoModelEdit.MailAddress = value;
                OnPropertyChanged("MailAddressEdit");
            }
        }
        public string PhoneNumberEdit
        {
            get { return _personInfoModelEdit.PhoneNumber; }
            set
            {
                Error = "";
                _personInfoModelEdit.PhoneNumber = value;
                OnPropertyChanged("PhoneNumberEdit");
            }
        }
        public string FirstNameEdit
        {
            get { return _personInfoModelEdit.FirstName; }
            set
            {
                Error = "";
                _personInfoModelEdit.FirstName = value;
                OnPropertyChanged("FirstNameEdit");
            }
        }
        public string LastNameEdit
        {
            get { return _personInfoModelEdit.LastName; }
            set
            {
                Error = "";
                _personInfoModelEdit.LastName = value;
                OnPropertyChanged("LastNameEdit");
            }
        }
        public DateTime BornDateEdit
        {
            get { return _personInfoModelEdit.BornDate; }
            set
            {
                Error = "";
                _personInfoModelEdit.BornDate = value.Date;
                OnPropertyChanged("BornDateEdit");
            }
        }
        private string _oldPassword;
        public string OldPassword
        {
            get { return _oldPassword; }
            set
            {
                Error = "";
                _oldPassword = value;
                OnPropertyChanged("OldPassword");
            }
        }
        
        //UserControl///////////////////////////////////////////////////////////////////////
        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged("CurrentView");
            }
        }

        private object _currentMainUCView;
        public object CurrentMainUCView
        {
            get { return _currentMainUCView; }
            set
            {
                _currentMainUCView = value;
                OnPropertyChanged("CurrentMainUCView");
            }
        }

        private object _currentPost;
        public object CurrentPost
        {
            get { return _currentPost; }
            set
            {
                _currentPost = value;
                OnPropertyChanged("CurrentPost");
            }
        }

        private object _currentElement;
        public object CurrentElement
        {
            get { return _currentElement; }
            set
            {
                _currentElement = value;
                OnPropertyChanged("CurrentElement");
            }
        }

        private object _comments;
        public object Comments
        {
            get { return _comments; }
            set
            {
                _comments = value;
                OnPropertyChanged("CurrentElement");
            }
        }

        private object _currentUserInfo;
        public object CurrentUserInfo
        {
            get { return _currentUserInfo; }
            set
            {
                _currentUserInfo = value;
                OnPropertyChanged("CurrentUserInfo");
            }
        }
        //Error////////////////////////////////////////////////////////////////////////////
        private string _error;
        public string Error
        {
            get { return _error; }
            set
            {
                _error = value;
                OnPropertyChanged("Error");
            }
        }

        //Clicks////////////////////////////////////////////////////////////////////////////
        private ICommand _gotoSignInUCCommand;
        public ICommand GotoSignInUCCommand
        {
            get
            {
                return _gotoSignInUCCommand ?? (_gotoSignInUCCommand = new RelayCommand(
                   async x =>
                   {
                       await ClearUserAsync();
                       CurrentView = SignInUC;
                   }));
            }
        }

        private ICommand _gotoSignUpUCCommand;
        public ICommand GotoSignUpUCCommand
        {
            get
            {
                return _gotoSignUpUCCommand ?? (_gotoSignUpUCCommand = new RelayCommand(
                   async x =>
                   {
                       await ClearUserAsync();
                       CurrentView = SignUpUC;
                   }));
            }
        }

        private ICommand _gotoMainUCUCCommand;
        public ICommand GotoMainUCUCCommand
        {
            get
            {
                return _gotoMainUCUCCommand ??
                     (_gotoMainUCUCCommand = new RelayCommand(async obj =>
                     {
                         try
                         {
                             
                             if (await UserBll.LogInAsync(Nickname, Password) == 1)
                             {
                                 Error = "";
                                 CurrentView = MainUC;
                                 GotoMyProfileUCCommand.Execute(new object());
                             }
                             else
                             {
                                 Error = "Wrong information error.";
                             }
                         }
                         catch
                         {
                             Error = "You need to enter some information.";
                         }

                     }));
            }
        }

        private ICommand _signUp;
        public ICommand SignUp
        {
            get
            {
                return _signUp ?? (_signUp = new RelayCommand(
                   async x =>
                   {
                       try
                       {
                           if (await UserBll.SignUpAsync(UserBll.User) == 1)
                           {
                               await ClearUserAsync();
                           }
                           else
                           {
                               Error = "Error";
                           }
                       }
                       catch
                       {
                           Error = "Wrong information";
                       }
                   }));
            }
        }

        private ICommand _clear;
        public ICommand Clear
        {
            get
            {
                return _clear ?? (_clear = new RelayCommand(
                   async x =>
                   {
                       await ClearUserAsync();
                   }));
            }
        }

        private ICommand _gotoMyProfileUCCommand;
        public ICommand GotoMyProfileUCCommand
        {
            get
            {
                return _gotoMyProfileUCCommand ?? (_gotoMyProfileUCCommand = new RelayCommand(
                   async x =>
                   {
                       PostModels = await PostBll.GetPostsAsync(UserBll.User.Nickname);
                       Page = 1;
                       CurrentMainUCView = MyProfileUC;
                       CurrentElement = MyPostsUC;
                       CurrentPost = PostUC;
                       NumberOfPost = PostModels.Count;
                       SubscribersNumber = UserBll.User.SubscribersNicknames.Count;
                       FavoriteUsersNumber = UserBll.User.FavoriteUsersNicknames.Count;

                       if (await SetPostAsync(Page) == 1)
                       {

                       }
                       else
                       {
                           await ClearPostInfoAsync();
                           CurrentPost = EmptyUC;
                       }
                   }));
            }
        }

        private ICommand _gotoAddPostUCCommand;
        public ICommand GotoAddPostUCCommand
        {
            get
            {
                return _gotoAddPostUCCommand ?? (_gotoAddPostUCCommand = new RelayCommand(
                   async x =>
                   {
                       await ClearPostInfoAsync();
                       CurrentMainUCView = AddPostUC;
                   }));
            }
        }

        private ICommand _gotoFavoriteUsersPostsUCCommand;
        public ICommand GotoFavoriteUsersPostsUCCommand
        {
            get
            {
                return _gotoFavoriteUsersPostsUCCommand ?? (_gotoFavoriteUsersPostsUCCommand = new RelayCommand(
                   async x =>
                   {
                       PostModels = await PostBll.GetPostsAsync(UserBll.User.FavoriteUsersNicknames);
                       Page = 1;
                       CurrentMainUCView = FavoriteUsersPostUC;
                       CurrentPost = PostUC;
                       if (await SetPostAsync(Page) == 1)
                       {

                       }
                       else
                       {
                           await ClearPostInfoAsync();
                           CurrentPost = EmptyUC;
                       }
                   }));
            }
        }

        private ICommand _gotoAllPostsUCCommand;
        public ICommand GotoAllPostsUCCommand
        {
            get
            {
                return _gotoAllPostsUCCommand ?? (_gotoAllPostsUCCommand = new RelayCommand(
                   async x =>
                   {
                       PostModels = await PostBll.GetPostsAsync();
                       Page = 1;

                       if (await SetPostAsync(Page) == 1)
                       {
                           CurrentPost = PostUC;
                           CurrentMainUCView = AllPostsUC;

                       }
                       else
                       {
                           await ClearPostInfoAsync();
                           CurrentPost = EmptyUC;
                           CurrentMainUCView = AllPostsUC;
    
                       }
                   }));
            }
        }

        private ICommand _addPost;
        public ICommand AddPost
        {
            get
            {
                return _addPost ?? (_addPost = new RelayCommand(
                   async x =>
                   {
                       try
                       {
                           CurrentPostModel.PostOwnerNickname = UserBll.User.Nickname;
                           if (await PostBll.CtreatePostAsync(CurrentPostModel) == 1)
                           {
                               await ClearPostInfoAsync();
                           }
                           else
                           {
                               Error = "You need to enter some information.";
                           }
                       }
                       catch
                       {
                           Error = "Error";
                       }

                   }));
            }
        }

        private ICommand _clearPostInfo;
        public ICommand ClearPostInfo
        {
            get
            {
                return _clearPostInfo ?? (_clearPostInfo = new RelayCommand(
                   async x =>
                   {
                       await ClearPostInfoAsync();
                   }));
            }
        }

        private ICommand _nextPage;
        public ICommand NextPage
        {
            get
            {
                return _nextPage ?? (_nextPage = new RelayCommand(
                   async x =>
                   {
                       if (Page < PostModels.Count)
                       {
                           Page += 1;
                           await SetPostAsync(Page);
                       }
                   }));
            }
        }

        private ICommand _prevPage;
        public ICommand PrevPage
        {
            get
            {
                return _prevPage ?? (_prevPage = new RelayCommand(
                   async x =>
                   {
                       if (Page > 1)
                       {
                           Page -= 1;
                           await SetPostAsync(Page);
                       }
                   }));
            }
        }

        private ICommand _gotoEditNicknameUCCommand;
        public ICommand GotoEditNicknameUCCommand
        {
            get
            {
                return _gotoEditNicknameUCCommand ?? (_gotoEditNicknameUCCommand = new RelayCommand(
                   x =>
                   {
                       NicknameEdit = Nickname;
                       OnPropertyChanged("NicknameEdit");
                       CurrentElement = EditNicknameUC;
                   }));
            }
        }

        private ICommand _gotoEditPasswordUCCommand;
        public ICommand GotoEditPasswordUCCommand
        {
            get
            {
                return _gotoEditPasswordUCCommand ?? (_gotoEditPasswordUCCommand = new RelayCommand(
                   x =>
                   {
                       CurrentElement = EditPasswordUC;
                   }));
            }
        }

        private ICommand _gotoEditProfileInfoUCCommand;
        public ICommand GotoEditProfileInfoUCCommand
        {
            get
            {
                return _gotoEditProfileInfoUCCommand ?? (_gotoEditProfileInfoUCCommand = new RelayCommand(
                   x =>
                   {
                       _personInfoModelEdit = UserBll.User.PersonInfo;
                       OnPropertyChanged("PersonInfoModelEdit");
                       CurrentElement = EditProfileInfoUC;
                   }));
            }
        }

        private ICommand _backToPost;
        public ICommand BackToPost
        {
            get
            {
                return _backToPost ?? (_backToPost = new RelayCommand(
                   x =>
                   {
                       GotoMyProfileUCCommand.Execute(new object());
                   }));
            }
        }

        private ICommand _editNickname;
        public ICommand EditNicknameCommand
        {
            get
            {
                return _editNickname ?? (_editNickname = new RelayCommand(
                   async x =>
                   {
                       try
                       {
                           if (await UserBll.EditNickameAsync(NicknameEdit) != null)
                           {
                               NicknameEdit = null;
                               OnPropertyChanged("NicknameEdit");
                               GotoMyProfileUCCommand.Execute(new object());
                               OnPropertyChanged("Nickname");
                           }
                           else
                           {
                               Error = "Error";
                           }
                       }
                       catch
                       {
                           Error = "Wrong information";
                       }
                   }));
            }
        }

        private ICommand _editPasswordCommand;
        public ICommand EditPasswordCommand
        {
            get
            {
                return _editPasswordCommand ?? (_editPasswordCommand = new RelayCommand(
                  async x =>
                  {
                      try
                      {
                          if (await UserBll.EditPasswordAsync(OldPassword,PasswordEdit) != null)
                          {
                              OldPassword = null;
                              OnPropertyChanged("OldPassword");
                              PasswordEdit = null;
                              OnPropertyChanged("PasswordEdit");                          
                              GotoMyProfileUCCommand.Execute(new object());
                              OnPropertyChanged("EditPasswordCommand");

                          }
                          else
                          {
                              Error = "Error";
                          }
                      }
                      catch
                      {
                          Error = "Wrong information";
                      }
                  }));
            }
        }

        private ICommand _editProfile;
        public ICommand EditProfileCommand
        {
            get
            {
                return _editProfile ?? (_editProfile = new RelayCommand(
                   async x =>
                   {
                       try
                       {
                           if (await UserBll.EditPfofileAsync(_personInfoModelEdit) != null)
                           {
                               FirstName = _personInfoModelEdit.FirstName;
                               LastName = _personInfoModelEdit.LastName;
                               MailAddress = _personInfoModelEdit.MailAddress;
                               PhoneNumber = _personInfoModelEdit.PhoneNumber;
                               BornDate = _personInfoModelEdit.BornDate;
                               OnPropertyChanged("PersonInfoModelEdit");
                             

                               GotoMyProfileUCCommand.Execute(new object());
                           }
                           else
                           {
                               Error = "Error";
                           }
                       }
                       catch
                       {
                           Error = "Wrong information";
                       }
                   }));
            }
        }

        private ICommand _likeCommand;
        public ICommand LikeCommand
        {
            get
            {
                return _likeCommand ?? (_likeCommand = new RelayCommand(
                   async x =>
                   {
                       if(await PostBll.LikePostAsync(CurrentPostModel.PostOwnerNickname,UserBll.User.Nickname, CurrentPostModel.Date)==1)
                       {
                           CurrentPostModel.Dislikes = await PostBll.GetPostDislikersAsync(CurrentPostModel.PostOwnerNickname, CurrentPostModel.Date);
                           NumberOfDislikes = CurrentPostModel.Dislikes.Count;
                           CurrentPostModel.Likes = await PostBll.GetPostLikersAsync(CurrentPostModel.PostOwnerNickname, CurrentPostModel.Date);
                           NumberOfLikes = CurrentPostModel.Likes.Count;
                       }
                       OnPropertyChanged("LikeCommand");
                   }));
            }
        }

        private ICommand _dislikeCommand;
        public ICommand DislikeCommand
        {
            get
            {
                return _dislikeCommand ?? (_dislikeCommand = new RelayCommand(
                   async x =>
                   {
                       if (await PostBll.DislikePostAsync(CurrentPostModel.PostOwnerNickname, UserBll.User.Nickname, CurrentPostModel.Date) == 1)
                       {
                           CurrentPostModel.Dislikes = await PostBll.GetPostDislikersAsync(CurrentPostModel.PostOwnerNickname, CurrentPostModel.Date);
                           NumberOfDislikes = CurrentPostModel.Dislikes.Count;
                           CurrentPostModel.Likes = await PostBll.GetPostLikersAsync(CurrentPostModel.PostOwnerNickname, CurrentPostModel.Date);
                           NumberOfLikes = CurrentPostModel.Likes.Count;
                       }
                       OnPropertyChanged("DislikeCommand");
                   }));
            }
        }

        private ICommand _undoCommand;
        public ICommand UndoCommand
        {
            get
            {
                return _undoCommand ?? (_undoCommand = new RelayCommand(
                   async x =>
                   {
                       if (await PostBll.UndoFeelingAsync(CurrentPostModel.PostOwnerNickname, UserBll.User.Nickname, CurrentPostModel.Date) !=null)
                       {
                           CurrentPostModel.Dislikes = await PostBll.GetPostDislikersAsync(CurrentPostModel.PostOwnerNickname, CurrentPostModel.Date);
                           NumberOfDislikes = CurrentPostModel.Dislikes.Count;
                           CurrentPostModel.Likes = await PostBll.GetPostLikersAsync(CurrentPostModel.PostOwnerNickname, CurrentPostModel.Date);
                           NumberOfLikes = CurrentPostModel.Likes.Count;
                       }
                       OnPropertyChanged("UndoCommand");
                   }));
               
            }
        }

        private ICommand _filterPostsCommand;
        public ICommand FilterPostsCommand
        {
            get
            {
                return _filterPostsCommand ?? (_filterPostsCommand = new RelayCommand(
                   async x =>
                   {
                       if(FilterNickname!="" && FilterNickname!=null)
                       {
                           Page = 1;
                           PostModels = await PostBll.GetPostsAsync(FilterNickname);
                           await SetPostAsync(Page);
                       }
                       else
                       {
                           GotoAllPostsUCCommand.Execute(new object());
                       }
                   }));

            }
        }

        private ICommand _nextCommentPage;
        public ICommand NextCommentPage
        {
            get
            {
                return _nextCommentPage ?? (_nextCommentPage = new RelayCommand(
                   async x =>
                   {
                       if (CommentPage < CommentModels.Count)
                       {
                           CommentPage += 1;
                           await SetCommentAsync(CommentPage);
                       }
                   }));
            }
        }

        private ICommand _prewCommentPage;
        public ICommand PrewCommentPage
        {
            get
            {
                return _prewCommentPage ?? (_prewCommentPage = new RelayCommand(
                   async x =>
                   {
                       if (CommentPage > 1)
                       {
                           CommentPage -= 1;
                           await SetCommentAsync(CommentPage);
                       }
                   }));
            }
        }

        private ICommand _closeAddComment;
        public ICommand CloseAddComment
        {
            get
            {
                return _closeAddComment ?? (_closeAddComment = new RelayCommand(
                   async x =>
                   {
                       await SetCommentAsync(CommentPage);
                       Comments = CommentsUC;
                       OnPropertyChanged("Comments");
                   }));
            }
        }

        private ICommand _gotoAddComment;
        public ICommand GotoAddComment
        {
            get
            {
                return _gotoAddComment ?? (_gotoAddComment = new RelayCommand(
                   x =>
                   {
                       CommentText = null;
                       CommentDate = DateTime.Now;
                       OnPropertyChanged("CommentDate");
                       Comments = AddCommentUC;
                       OnPropertyChanged("Comments");
                   }));
            }
        }

        private ICommand _addComment;
        public ICommand AddComment
        {
            get
            {
                return _addComment ?? (_addComment = new RelayCommand(
                   async x =>
                   {
                       try
                       {
                           if (await PostBll.CreateCommentAsync(CommentText, CurrentPostModel.PostOwnerNickname, UserBll.User.Nickname,CurrentPostModel.Date) !=null)
                           {
                               CurrentPostModel.Comments = await PostBll.GetPostCommentAsync(CurrentPostModel.PostOwnerNickname, CurrentPostModel.Date);
                               await SetPostAsync(Page);
                               OnPropertyChanged("Posts");
                           }
                           else
                           {
                               Error = "You need to enter some information.";
                           }
                       }
                       catch
                       {
                           Error = "Error";
                       }
                   }));
            }
        }

        private ICommand _gotoDeleteFavoriteUser;
        public ICommand GotoDeleteFavoriteUser
        {
            get
            {
                return _gotoDeleteFavoriteUser ?? (_gotoDeleteFavoriteUser = new RelayCommand(
                   x =>
                   {
                       CurrentElement = DeleteFavoriteUserUC;
                   }));
            }
        }

        private ICommand _gotoAddFavoriteUser;
        public ICommand GotoAddFavoriteUser
        {
            get
            {
                return _gotoAddFavoriteUser ?? (_gotoAddFavoriteUser = new RelayCommand(
                   x =>
                   {
                       CurrentElement = AddFavoriteUserUC;
                   }));
            }
        }

        private ICommand _addFavoriteUserCommand;
        public ICommand AddFavoriteUserCommand
        {
            get
            {
                return _addFavoriteUserCommand ?? (_addFavoriteUserCommand = new RelayCommand(
                   async x =>
                   {
                       try
                       {
                           if (await UserBll.FolowAsync(SelectedNickname) == 1)
                           {
                               SelectedNickname = "";
                               GotoMyProfileUCCommand.Execute(new object());
                           }
                           else
                           {
                               Error = "Bad nickname.";
                           }
                       }
                       catch
                       {
                           Error = "Error";
                       }
                   }));
            }
        }

        private ICommand _removeFavoriteUserCommand;
        public ICommand RemoveFavoriteUserCommand
        {
            get
            {
                return _removeFavoriteUserCommand ?? (_removeFavoriteUserCommand = new RelayCommand(
                   async x =>
                   {
                       try
                       {
                           if (await UserBll.UnfolowAsync(SelectedNickname) == 1)
                           {
                               SelectedNickname = "";
                               GotoMyProfileUCCommand.Execute(new object());
                           }
                           else
                           {
                               Error = "Bad nickname.";
                           }
                       }
                       catch
                       {
                           Error = "Error";
                       }
                   }));
            }
        }

        private ICommand _gotoFavoriteUser;
        public ICommand GotoFavoriteUser
        {
            get
            {
                return _gotoFavoriteUser ?? (_gotoFavoriteUser = new RelayCommand(
                   async x =>
                   {
                       UserNicknames = UserBll.User?.FavoriteUsersNicknames as List<string> ?? new List<string>();
                       PageUserInfo = 1;
                       CurrentElement = ShowUserInfoUC;
                       CurrentUserInfo = UserProfileUC;
                       if (await SetUserInfo(PageUserInfo) == 1)
                       {

                       }
                       else
                       {
                           CurrentUserInfo = EmptyUC;
                       }
                   }));
            }
        }

        private ICommand _gotoSubscribers;
        public ICommand GotoSubscribers
        {
            get
            {
                return _gotoSubscribers ?? (_gotoSubscribers = new RelayCommand(
                   async x =>
                   {
                       UserNicknames = UserBll.User?.SubscribersNicknames as List<string> ?? new List<string>();
                       PageUserInfo = 1;
                       CurrentElement = ShowUserInfoUC;
                       CurrentUserInfo = UserProfileUC;
                       if (await SetUserInfo(PageUserInfo) == 1)
                       {

                       }
                       else
                       {
                           CurrentUserInfo = EmptyUC;
                       }
                   }));
            }
        }

        private ICommand _prevPageUserInf;
        public ICommand PrevPageUserInf
        {
            get
            {
                return _prevPageUserInf ?? (_prevPageUserInf = new RelayCommand(
                   async x =>
                   {
                       if (PageUserInfo > 1)
                       {
                           PageUserInfo -= 1;
                           await SetUserInfo(PageUserInfo);
                       }
                   }));
            }
        }

        private ICommand _nextPageUserInf;
        public ICommand NextPageUserInf
        {
            get
            {
                return _nextPageUserInf ?? (_nextPageUserInf = new RelayCommand(
                   async x =>
                   {
                       if (PageUserInfo < UserNicknames.Count)
                       {
                           PageUserInfo += 1;
                           await SetUserInfo(PageUserInfo);
                       }
                   }));
            }
        }
        //Methods////////////////////////////////////////////////////////////////////////////
        public MainWindowViewModel()
        {
            CurrentView = SignInUC;
        }

        private async Task ClearUserAsync()
        {
            await Task.Run(() =>
            {
                UserBll.User.SubscribersNicknames = null;
                UserBll.User.FavoriteUsersNicknames = null;
                Nickname = null;
                Password = null;
                FirstName = null;
                LastName = null;
                MailAddress = null;
                PhoneNumber = null;
                Error = null;
                OnPropertyChanged("Clear");
            });
        }
        private async Task ClearPostInfoAsync()
        {
            await Task.Run(() =>
            {
                Page = 1;
                PostOwnerNickname = null;
                Text = null;
                PicturePath = null;
                PostDate = DateTime.Now;
                CurrentPostModel.Likes = null;
                CurrentPostModel.Dislikes = null;
                CurrentPostModel.Comments = null;
                OnPropertyChanged("Clear");
            });
        }
        private async Task<int> SetPostAsync(int page)
        {
            return await Task.Run(async () =>
            {
                if (page <= PostModels.Count)
                {
                    CurrentPostModel = PostModels[Page - 1];
                    CommentModels = CurrentPostModel?.Comments as List<CommentModel> ?? new List<CommentModel>();
                    CommentPage = 1;

                    Comments = CommentsUC;
                    await SetCommentAsync(CommentPage);
                    OnPropertyChanged("Comments");

                    PageNumeration = $"{page}/{PostModels.Count}";
                    OnPropertyChanged("PageNumeration");
                    OnPropertyChanged("PostOwnerNickname");
                    OnPropertyChanged("Text");
                    OnPropertyChanged("PicturePath");
                    OnPropertyChanged("PostDate");
                    OnPropertyChanged("SetPost");
                    NumberOfDislikes = CurrentPostModel.Dislikes.Count;
                    NumberOfLikes = CurrentPostModel.Likes.Count;
                    OnPropertyChanged("NumberOfDislikes");
                    OnPropertyChanged("NumberOfLikes");

                    return 1;
                }
                PageNumeration = $"{0}/{PostModels.Count}";
                OnPropertyChanged("PageNumeration");
                return 0;
            });
        }

        private async Task<int> SetCommentAsync(int page)
        {
            return await Task.Run(() =>
            {
                if (page <= CommentModels.Count )
                {
                    CommentPageNumeration = $"{page}/{CommentModels.Count}";
                    OnPropertyChanged("CommentPageNumeration");
                    CurrentCommentModel = CommentModels[page - 1];
                    OnPropertyChanged("CommentText");
                    OnPropertyChanged("CommentDate");
                    OnPropertyChanged("CommentOwner");
                    return 1;
                }
                CurrentCommentModel = new CommentModel() { Date = DateTime.Now, Text = "No comments :(", CommentOwnerNickname = "Admin" };
                OnPropertyChanged("CommentText");
                OnPropertyChanged("CommentDate");
                OnPropertyChanged("CommentOwner");
                CommentPageNumeration = $"{0}/{0}";
                OnPropertyChanged("CommentPageNumeration");
                return 0;
            });
        }

        private async Task<int> SetUserInfo(int page)
        {
            return await Task.Run(async () =>
            {
                if (page <= UserNicknames.Count)
                {
                    PageNumerationUserInfo = $"{page}/{UserNicknames.Count}";
                    OnPropertyChanged("PageNumerationUserInfo");
                    CurrentUserProfilModel = await UserBll.GetProfileInfoAsync(UserNicknames[page - 1]);
                    NicknameUser = UserNicknames[page - 1];
                    OnPropertyChanged("NicknameUser");
                    OnPropertyChanged("FirstNameUser");
                    OnPropertyChanged("LastNameUser");
                    OnPropertyChanged("BornDateUser");
                    OnPropertyChanged("PhoneNumberUser");
                    OnPropertyChanged("MailAddressUser");

                    return 1;
                }
                PageNumerationUserInfo = $"{0}/{0}";
                OnPropertyChanged("PageNumerationUserInfo");
                return 0;
            });
        }
    }
}