using System;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WalkingAround;
using WalkingAround.Models;

namespace WalkingTest
{
    /// <summary>
    /// Interaction logic for Other.xaml
    /// </summary>
    public partial class ModelView : UserControl
    {
        private const string resourcetype = "model";
        private Model _this;

        public Model ModelContext { get { return _this; } }

        public int Type { get { return int.Parse(_this.Key); } }

        public ModelView(int type = 0)
        {
            _this = new Model(type);
            _this.PropertyChanged += Source_PropertyChanged;
            InitializeComponent();

            ((ImageBrush)this.polyModel.Fill).ImageSource = new BitmapImage(new Uri(StaticResources.Get(_this.Type, this.Type)));
        }

        private void Source_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Container":
                    ContainerChanged();
                    break;

                case "Type":
                    TypeChanged();
                    break;

                default:
                    break;
            }
        }

        private void ContainerChanged()
        {
            (Parent as Node).RemoveModel(this);
            (Parent as Node).AddModel(this);
        }

        private void TypeChanged()
        {
            ((ImageBrush)this.polyModel.Fill).ImageSource = new BitmapImage(new Uri(StaticResources.Get(resourcetype, this.Type)));
        }
    }
}