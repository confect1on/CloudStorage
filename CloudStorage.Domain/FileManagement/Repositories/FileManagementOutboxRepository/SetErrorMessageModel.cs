﻿using CloudStorage.Domain.FileManagement.ValueObjects;

namespace CloudStorage.Domain.FileManagement.Repositories.FileManagementOutboxRepository;

public record SetErrorMessageModel(FileManagementOutboxId FileManagementOutboxId, string ErrorMessage);
