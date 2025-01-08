﻿using Krosoft.Extensions.Cqrs.Models.Queries;
using Krosoft.Extensions.Samples.Library.Models.Dto;

namespace Krosoft.Extensions.Samples.Library.Models.Queries;

public record LanguesQuery : BaseQuery<IEnumerable<LangueDto>>;