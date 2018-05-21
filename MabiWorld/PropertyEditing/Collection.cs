using System;
using System.ComponentModel.Design;
using System.Windows.Forms;

namespace MabiWorld.PropertyEditing
{
	/// <summary>
	/// Provides a collection editor with an event that is raised when
	/// the collection or one of its non-collection properties changed.
	/// </summary>
	public class NotifyingCollectionEditor : CollectionEditor
	{
		/// <summary>
		/// Raised when the collection changes.
		/// </summary>
		public static event EventHandler CollectionChanged;

		/// <summary>
		/// Raised when a property in the collection changes.
		/// </summary>
		public static event EventHandler<PropertyValueChangedEventArgs> CollectionPropertyChanged;

		/// <summary>
		/// Raised when the collection editor form is closed.
		/// </summary>
		public static event FormClosedEventHandler FormClosed;

		/// <summary>
		/// Creates new instance.
		/// </summary>
		/// <param name="type"></param>
		public NotifyingCollectionEditor(Type type)
			: base(type)
		{
		}

		/// <summary>
		/// Creates new collection form and hooks up events.
		/// </summary>
		/// <returns></returns>
		protected override CollectionForm CreateCollectionForm()
		{
			var form = base.CreateCollectionForm();
			var table = ((form as Form).Controls[0] as TableLayoutPanel);

			if (table != null)
			{
				// Hook up property changed event
				if (table.Controls[5] is PropertyGrid propertyGrid)
					propertyGrid.PropertyValueChanged += (sender, args) => CollectionPropertyChanged?.Invoke(this, args);

				// Hook up collection changed event to add and remove buttons
				if (table.Controls[1].Controls[0] is Button addButton)
					addButton.Click += (sender, args) => CollectionChanged?.Invoke(this, null);
				if (table.Controls[1].Controls[1] is Button removeButton)
					removeButton.Click += (sender, args) => CollectionChanged?.Invoke(this, null);

				// Hook up form closed event
				form.FormClosed += (sender, args) => FormClosed?.Invoke(this, args);
			}

			return form;
		}
	}
}
