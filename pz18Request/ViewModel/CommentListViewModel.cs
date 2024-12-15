using pz18Request.Model;
using pz18Request.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace pz18Request.ViewModel
{
    class CommentListViewModel : BindableBase
    {
        private ICommentRepository _repository;

        public CommentListViewModel(ICommentRepository repository)
        {
            _repository = repository;
            Comments = new ObservableCollection<Comment>();
            LoadComment();
        }

        private bool _isEditMode;

        public bool isEditMode
        {
            get => _isEditMode;
            set => SetProperty(ref _isEditMode, value);
        }

        private ObservableCollection<Comment>? _comments;
        public ObservableCollection<Comment>? Comments
        {
            get => _comments;
            set => SetProperty(ref _comments, value);
        }

        public ObservableCollection<Request> _request;
        public ObservableCollection<Request>? Requests
        {
            get => _request;
            set => SetProperty(ref _request, value);
        }

        public ICommand LoadCommentCommand { get; }

        private List<Comment>? _commentList;
        public RelayCommand<Request> OpenOrderViewCommand { get; private set; }

        public event Action<Request> OpenOrderViewRequested = delegate { };
        public async void LoadComment()
        {
            Comments.Clear();
            var comments  = new ObservableCollection<Comment>(await _repository.GetCommentAsync());
            foreach (var comment in comments)
            {
                Comments.Add(comment);
            }
        }

        public async void LoadCommentRequest(int requestId)
        {
            Comments.Clear();
            var comments = await _repository.GetCommentByRequestAsync(requestId);
            foreach (var comment in comments)
            {
                Comments.Add(comment);
            }
        }
    }
}
