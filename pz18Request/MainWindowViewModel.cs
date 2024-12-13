using pz18Request.Model;
using pz18Request.Services;
using pz18Request.ViewModel;
using Unity;

namespace pz18Request
{
    internal class MainWindowViewModel : BindableBase
    {
        private RequestListViewModel _requestListVM;
        private AddEditRequestViewModel _addEditRequestVM;
        private CommentListViewModel _commentListVM;

        public MainWindowViewModel()
        {
            NavigationCommand = new RelayCommand<string>(OnNavigation);
            _requestListVM = new RequestListViewModel(new RequestRepository());
            _commentListVM = new CommentListViewModel(new CommentRepository());
            _addEditRequestVM = RepoContainer.Container.Resolve<AddEditRequestViewModel>();
            _commentListVM = RepoContainer.Container.Resolve<CommentListViewModel>();
            _requestListVM.AddRequestRequested += NavigationAddRequest;
            _requestListVM.EditRequestRequested += NavigationUpdateRequest;
            _addEditRequestVM.Done += NavigationToListRequest;
            _requestListVM.CheckCommentsRequested += NavigationToCommentRequest;
        }

        public BindableBase _currentViewModel;
        public BindableBase CurrentViewModel
        {
            get => _currentViewModel;
            set => SetProperty(ref _currentViewModel, value);
        }
        public RelayCommand<string> NavigationCommand { get; private set; }

        private void OnNavigation(string dest)
        {
            switch (dest)
            {
                case "requestList":
                    CurrentViewModel = _requestListVM; break;
                case "commentList":
                    CurrentViewModel = _commentListVM; break;
            }
        }

        private void NavigationUpdateRequest(Request request)
        {
            _addEditRequestVM.IsEditMode = true;
            _addEditRequestVM.SetRequest(request);
            CurrentViewModel = _addEditRequestVM;
        }
        private void NavigationAddRequest()
        {
            _addEditRequestVM.IsEditMode = false;
            CurrentViewModel = _addEditRequestVM;
        }
        private void NavigationToListRequest()
        {
            _requestListVM.isEditMode = false;
            CurrentViewModel = _requestListVM;
        }
        private void NavigationToCommentRequest(Request request)
        {
            _commentListVM.LoadCommentRequest(request.RequestId);
            CurrentViewModel = _commentListVM;
        }
    }
}
