using System;

namespace FitMate.Presentation.Models.Bodyweight
{
    public record EditRecordsDto(DateTime[] Dates, float[] Weights);
}