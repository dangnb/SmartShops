using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Contract.Abstractions.Message;
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

    public DateTime CreatedAt { get; private set; }
    public string? CreatedBy { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public string? UpdatedBy { get; private set; }

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
        string? note,
        string? updatedBy)
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

        Touch(updatedBy);
    }

    public void UpdateContact(
        string? contactName,
        string? contactPhone,
        string? contactEmail,
        string? updatedBy)
    {
        ContactName = contactName?.Trim();
        ContactPhone = contactPhone?.Trim();
        ContactEmail = contactEmail?.Trim();

        Touch(updatedBy);
    }

    public void UpdateAddress(
        int? provinceId,
        int? wardId,
        string? addressLine,
        string? fullAddress,
        string? updatedBy)
    {
        ProvinceId = provinceId;
        WardId = wardId;
        AddressLine = addressLine?.Trim();
        FullAddress = fullAddress?.Trim();

        Touch(updatedBy);
    }

    public void UpdateBankInfo(
        string? bankName,
        string? bankAccountNo,
        string? bankAccountName,
        string? updatedBy)
    {
        BankName = bankName?.Trim();
        BankAccountNo = bankAccountNo?.Trim();
        BankAccountName = bankAccountName?.Trim();

        Touch(updatedBy);
    }

    public void ChangePaymentTerm(int paymentTermDays, string? updatedBy)
    {
        PaymentTermDays = paymentTermDays < 0 ? 0 : paymentTermDays;
        Touch(updatedBy);
    }

    public void Rate(byte rating, string? updatedBy)
    {
        if (rating < 1 || rating > 5)
            throw new ArgumentOutOfRangeException(nameof(rating), "Rating must be between 1 and 5.");

        Rating = rating;
        Touch(updatedBy);
    }

    public void Activate(string? updatedBy)
    {
        if (!IsActive)
        {
            IsActive = true;
            Touch(updatedBy);
        }
    }

    public void Deactivate(string? updatedBy)
    {
        if (IsActive)
        {
            IsActive = false;
            Touch(updatedBy);
        }
    }

    public void SoftDelete(string? updatedBy)
    {
        if (!IsDeleted)
        {
            IsDeleted = true;
            IsActive = false;
            Touch(updatedBy);
        }
    }

    private void Touch(string? updatedBy)
    {
        UpdatedAt = DateTime.UtcNow;
        UpdatedBy = updatedBy;
    }
}

