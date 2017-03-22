using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;

namespace Recipes.Domain
{
	[Serializable]
	public class Recipe : IComparable<Recipe>
	{
		#region Fields

		[NotMapped]
		static int _nextInstanceID = 0;
		[NotMapped]
		int _instanceID;

		#endregion

		#region Properties

		public int RecipeId { get; private set; }

		public string Name { get; set; }

		public string Uri { get; set; }

		public string Source { get; set; }

		public List<Tag> Tags { get; set; }

		public List<IngredientGroup> IngredientGroups { get; set; }

		public List<ProcedureGroup> ProcedureGroups { get; set; }

		public int? EthnicityId { get; set; }

		public int? Rating { get; set; }

		public TimeSpan? Time { get; set; }

		public string ImageUri { get; set; }

		[NotMapped]
		public bool IsValid
		{
			get
			{
				var result = false;

				result = (null != this.IngredientGroups && this.IngredientGroups.Select(x => x.Items).Count() > 0);

				if (result)
					result = (null != this.ProcedureGroups && this.ProcedureGroups.Select(x => x.Items).Count() > 0);

				if (result)
					result = !string.IsNullOrEmpty(Name);

				if (result)
					result = !string.IsNullOrEmpty(ImageUri);

				if (result)
					result = !string.IsNullOrEmpty(Source);


				return result;
			}
		}

		#endregion

		#region Construction

		public Recipe()
		{
			this.Init();
		}
		void Init()
		{
			this._instanceID = ++_nextInstanceID;
			if (null == this.Tags)
				this.Tags = new List<Tag>();
		}

		#endregion

		#region Methods

		[OnSerializing]
		void OnSerializing(StreamingContext ctx)
		{
		}

		[OnSerialized]
		void OnSerialized(StreamingContext ctx)
		{
		}

		[OnDeserializing]
		void OnDeserializing(StreamingContext ctx)
		{
		}

		[OnDeserialized]
		void OnDeserialized(StreamingContext ctx)
		{
			this.Init();
		}

		public int CompareTo(Recipe other)
		{
			var result = this.Name.CompareTo(other.Name);
			return result;
		}

		List<string> GetText<T>(List<GroupBase<T>> groups) where T : GroupItemBase, new()
		{
			var result = new List<string>();
			foreach (var g in groups)
			{
				if (!string.IsNullOrEmpty(g.Text))
					result.Add(g.Text);
				foreach (var i in g.Items)
				{
					if (!string.IsNullOrEmpty(i.Text))
						result.Add(i.Text);
				}
			}

			return result;
		}

		public List<string> GetIngredientStrings()
		{
			var result = new List<string>();
			result = this.GetText<IngredientItem>((dynamic)this.IngredientGroups);
			return result;
		}

		public List<string> GetProcedureStrings()
		{
			var result = new List<string>();
			result = this.GetText<ProcedureItem>((dynamic)this.ProcedureGroups);
			return result;
		}


		#endregion
	}//class
}//ns
