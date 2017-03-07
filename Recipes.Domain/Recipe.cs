﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Recipes.Domain
{
	[Serializable]
	public class Recipe : IComparable<Recipe>
	{
		static int _nextInstanceID = 0;
		int _instanceID;
		public int RecipeId { get; private set; }
		public string Name { get; set; }
		public string Uri { get; set; }
		public string Source { get; set; }
		public List<Tag> Tags { get; set; }

		public List<IngredientGroup> IngredientGroups { get; set; }
		public List<ProcedureGroup> ProcedureGroups { get; set; }

		public List<string> GetIngredientStrings()
		{
			var result = new List<string>();
			result = this.GetText<IngredientGroupItem>((dynamic)this.IngredientGroups);
			return result;
		} 
		public List<string> GetProcedureStrings()
		{
			var result = new List<string>();
			result = this.GetText<ProcedureGroupItem>((dynamic)this.ProcedureGroups);
			return result;
		}

		//[JsonIgnore]
		public int? EthnicityId { get; set; }
		public int? Rating { get; set; }
		public TimeSpan? Time { get; set; }

		public string ImageUri { get; set; }

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

		public bool IsValid
		{
			get
			{
				var result = false;

#warning **** FIXME ****
				//result = (null != Ingredients && Ingredients.Groups.Select(x => x.Items).Count() > 0);

				//if (result)
				//    result = (null != Procedure && Procedure.Groups.Select(x => x.Items).Count() > 0);

				if (result)
					result = !string.IsNullOrEmpty(Name);

				if (result)
					result = !string.IsNullOrEmpty(ImageUri);

				if (result)
					result = !string.IsNullOrEmpty(Source);


				return result;
			}
		}
		List<string> GetText<T>(List<GroupBase<T>> groups) where T: GroupItemBase
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

	}//class
}//ns
