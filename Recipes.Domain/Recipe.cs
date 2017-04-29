using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;

namespace Recipes.Domain
{
    [Serializable]
    public class Recipe : EntityBase<Recipe>, IComparable<Recipe>
    {
        #region Properties

        public int RecipeId { get; set; }

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

        [Conditional("RECIPE_ASSERT_ON_INVALID")]
        void AssertInvalid(bool isValid)
        {
            Debug.Assert(isValid, this.Uri);
        }

        [NotMapped]
        public bool IsValid
        {
            get
            {
                var result = false;

                result = (null != this.IngredientGroups && this.IngredientGroups.Select(x => x.Items).Count() > 0);
                if (!result)
                    AssertInvalid(result);

                if (result)
                {
                    result = (null != this.ProcedureGroups && this.ProcedureGroups.Select(x => x.Items).Count() > 0);
                    AssertInvalid(result);
                }

                if (result)
                {
                    result = !string.IsNullOrEmpty(Name);
                    AssertInvalid(result);
                }

                if (result)
                {
                    result = !string.IsNullOrEmpty(ImageUri);
                    AssertInvalid(result);
                }

                if (result)
                {
                    result = !string.IsNullOrEmpty(Source);
                    AssertInvalid(result);
                }

                return result;
            }
        }

        public override int PrimaryKey
        {
            get
            {
                return RecipeId;
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

        List<string> GetText<T, I>(List<GroupBase<T, I>> groups) where I : GroupItemBase<I>, new()
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
            result = this.GetText<IngredientGroup, IngredientItem>((dynamic)this.IngredientGroups);
            return result;
        }

        public List<string> GetProcedureStrings()
        {
            var result = new List<string>();
            result = this.GetText<ProcedureGroup, ProcedureItem>((dynamic)this.ProcedureGroups);
            return result;
        }


        #endregion
    }//class
}//ns
