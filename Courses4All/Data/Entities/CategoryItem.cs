
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Courses4All.Data.Entities
{
    public class CategoryItem
    {

        private DateTime _releaseDate = DateTime.MinValue;
        public int Id { get; set; }
        [Required]
        [StringLength(80,MinimumLength = 10)]
        public string Title {  get; set; }
        public string Description { get; set; }
        //Foreign key from Category
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}" , ApplyFormatInEditMode = true )]
        public DateTime DateTimeItemReleased
        {
            get
            {
                return (_releaseDate == DateTime.MinValue) ? DateTime.Now : _releaseDate;
            }
            set
            {
                _releaseDate = value;
            }
        }
        [NotMapped]
        public virtual ICollection<SelectListItem> MediaTypes { get; set; }
        //foreign key
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Please selecct a Valid Media Type")]
        public int MediaTypeId {  get; set; }

        [NotMapped]
        public int ContentId { get; set; }
    }
}
