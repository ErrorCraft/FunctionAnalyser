﻿using System.ComponentModel.DataAnnotations;

namespace FunctionAnalyser.Results
{
    public enum SortType
    {
        [Display(Name = "Times used")]
        TimesUsed = 0,
        [Display(Name = "Alphabetical")]
        Alphabetical = 1,
        [Display(Name = "Command length")]
        CommandLength = 2
    }
}
