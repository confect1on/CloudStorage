﻿using CloudStorage.FileService.Domain.FileManagement.ValueObjects;

namespace CloudStorage.FileService.Domain.FileManagement.Repositories.FileManagementOutboxRepository;

public record SetErrorMessageModel(FileManagementOutboxId FileManagementOutboxId, string ErrorMessage);
