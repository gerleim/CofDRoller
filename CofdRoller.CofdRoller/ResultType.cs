﻿using System.ComponentModel.DataAnnotations;

namespace CofdRoller;

public enum ResultType
{
    [Display(Name = "Undetermined")]
    Undetermined,
    [Display(Name = "Success")]
    Success,
    [Display(Name = "Exceptional Success")]
    ExceptionalSuccess,
    [Display(Name = "Failure")]
    Failure,
    [Display(Name = "Dramatic Failure")]
    DramaticFailure
}
