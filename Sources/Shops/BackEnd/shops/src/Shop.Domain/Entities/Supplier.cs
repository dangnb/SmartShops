using Shop.Domain.Abstractions;

namespace Shop.Domain.Entities;


// public class Supplier : AggregateRoot<int>  // nếu bạn có base
public class Supplier : EntityAuditBase<Guid>
{
    public string Code { get; private set; }          // NCC001
    public string Name { get; private set; }          // Công ty TNHH ABC
    public string? ShortName { get; private set; }    // ABC

    public string? TaxCode { get; private set; }
    public string? Phone { get; private set; }
    public string? Email { get; private set; }
    public string? Website { get; private set; }

    // ===== Contact (trải phẳng) =====
    public string? ContactName { get; private set; }
    public string? ContactPhone { get; private set; }
    public string? ContactEmail { get; private set; }

    // ===== Địa chỉ (trải phẳng, dùng Id danh mục) =====
    public int? ProvinceId { get; private set; }
    public int? WardId { get; private set; }

    public string? AddressLine { get; private set; }   // Số nhà, tên đường
    public string? FullAddress { get; private set; }   // Cache hiển thị

    // ===== Ngân hàng (trải phẳng) =====
    public string? BankName { get; private set; }
    public string? BankAccountNo { get; private set; }
    public string? BankAccountName { get; private set; }

    // ===== Khác =====
    public int PaymentTermDays { get; private set; }

    public byte? Rating { get; private set; }          // 1–5
    public string? Note { get; private set; }

    public bool IsActive { get; private set; }
    public bool IsDeleted { get; private set; }

    // ===== ctor cho EF =====
    private Supplier() { }

    // ===== ctor private dùng trong factory =====
    private Supplier(
        string code,
        string name,
        string? shortName,
        string? taxCode,
        string? phone,
        string? email,
        string? website,
        string? contactName,
        string? contactPhone,
        string? contactEmail,
        int? provinceId,
        int? wardId,
        string? addressLine,
        string? fullAddress,
        string? bankName,
        string? bankAccountNo,
        string? bankAccountName,
        int paymentTermDays,
        string? note,
        string? createdBy)
    {
        if (string.IsNullOrWhiteSpace(code))
            throw new ArgumentException("Mã nhà cung cấp khong được để trống .", nameof(code));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Tên nhà cung cấp không được để trống.", nameof(name));

        Code = code.Trim();
        Name = name.Trim();
        ShortName = shortName?.Trim();

        TaxCode = taxCode?.Trim();
        Phone = phone?.Trim();
        Email = email?.Trim();
        Website = website?.Trim();

        ContactName = contactName?.Trim();
        ContactPhone = contactPhone?.Trim();
        ContactEmail = contactEmail?.Trim();

        ProvinceId = provinceId;
        WardId = wardId;
        AddressLine = addressLine?.Trim();
        FullAddress = fullAddress?.Trim();

        BankName = bankName?.Trim();
        BankAccountNo = bankAccountNo?.Trim();
        BankAccountName = bankAccountName?.Trim();

        PaymentTermDays = paymentTermDays < 0 ? 0 : paymentTermDays;

        Note = note;

        IsActive = true;
        IsDeleted = false;

        CreatedAt = DateTime.UtcNow;
        CreatedBy = createdBy;
    }

    // ===== Factory =====
    public static Supplier Create(
        string code,
        string name,
        string? shortName,
        string? taxCode,
        string? phone,
        string? email,
        string? website,
        string? contactName,
        string? contactPhone,
        string? contactEmail,
        int? provinceId,
        int? wardId,
        string? addressLine,
        string? fullAddress,
        string? bankName,
        string? bankAccountNo,
        string? bankAccountName,
        int paymentTermDays,
        string? note,
        string? createdBy)
        => new Supplier(
            code,
            name,
            shortName,
            taxCode,
            phone,
            email,
            website,
            contactName,
            contactPhone,
            contactEmail,
            provinceId,
            wardId,
            addressLine,
            fullAddress,
            bankName,
            bankAccountNo,
            bankAccountName,
            paymentTermDays,
            note,
            createdBy);

    // ===== Domain methods =====

    public void UpdateBasicInfo(
        string name,
        string? shortName,
        string? phone,
        string? email,
        string? website,
        string? taxCode,
        string? note)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Supplier name is required.", nameof(name));

        Name = name.Trim();
        ShortName = shortName?.Trim();
        Phone = phone?.Trim();
        Email = email?.Trim();
        Website = website?.Trim();
        TaxCode = taxCode?.Trim();
        Note = note;
    }

    public void UpdateContact(
        string? contactName,
        string? contactPhone,
        string? contactEmail)
    {
        ContactName = contactName?.Trim();
        ContactPhone = contactPhone?.Trim();
        ContactEmail = contactEmail?.Trim();

    }

    public void UpdateAddress(
        int? provinceId,
        int? wardId,
        string? addressLine,
        string? fullAddress)
    {
        ProvinceId = provinceId;
        WardId = wardId;
        AddressLine = addressLine?.Trim();
        FullAddress = fullAddress?.Trim();

    }

    public void UpdateBankInfo(
        string? bankName,
        string? bankAccountNo,
        string? bankAccountName)
    {
        BankName = bankName?.Trim();
        BankAccountNo = bankAccountNo?.Trim();
        BankAccountName = bankAccountName?.Trim();
    }

    public void ChangePaymentTerm(int paymentTermDays)
    {
        PaymentTermDays = paymentTermDays < 0 ? 0 : paymentTermDays;
    }

    public void Rate(byte rating)
    {
        if (rating < 1 || rating > 5)
            throw new ArgumentOutOfRangeException(nameof(rating), "Rating must be between 1 and 5.");

        Rating = rating;
    }

    public void Activate()
    {
        if (!IsActive)
        {
            IsActive = true;
        }
    }

    public void Deactivate()
    {
        if (IsActive)
        {
            IsActive = false;
        }
    }

    public void SoftDelete()
    {
        if (!IsDeleted)
        {
            IsDeleted = true;
            IsActive = false;
        }
    }
}

