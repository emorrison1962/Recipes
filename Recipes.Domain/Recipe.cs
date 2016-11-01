using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.Domain
{
	[Serializable]
	public class Recipe
	{
		static int _nextInstanceID = 0;
		int _instanceID;
		public int RecipeId { get; private set; }
		public string Name { get; set; }
		public string Uri { get; set; }
		public string Source { get; set; }
		public List<Tag> Tags { get; set; }

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

	}
}
