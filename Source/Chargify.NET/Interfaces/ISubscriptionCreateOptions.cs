﻿#region License, Terms and Conditions
//
// ISubscriptionCreateOptions.cs
//
// Authors: Kori Francis <twitter.com/djbyter>, David Ball
// Copyright (C) 2010 Clinical Support Systems, Inc. All rights reserved.
// 
//  THIS FILE IS LICENSED UNDER THE MIT LICENSE AS OUTLINED IMMEDIATELY BELOW:
//
//  Permission is hereby granted, free of charge, to any person obtaining a
//  copy of this software and associated documentation files (the "Software"),
//  to deal in the Software without restriction, including without limitation
//  the rights to use, copy, modify, merge, publish, distribute, sublicense,
//  and/or sell copies of the Software, and to permit persons to whom the
//  Software is furnished to do so, subject to the following conditions:
//
//  The above copyright notice and this permission notice shall be included in
//  all copies or substantial portions of the Software.
//
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
//  FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
//  DEALINGS IN THE SOFTWARE.
//
#endregion

namespace ChargifyNET
{
    #region Imports
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    #endregion

    public interface ISubscriptionCreateOptions
    {
        /// <summary>
        /// The API Handle of the product for which you are creating a subscription. 
        /// Required, unless a product_id is given instead.
        /// </summary>
        string ProductHandle { get; set; }

        /// <summary>
        /// The Product ID of the product for which you are creating a subscription. 
        /// The product ID is not currently published, so we recommend using the API 
        /// Handle instead.
        /// </summary>
        int? ProductID { get; set; }

        /// <summary>
        /// The ID of an existing customer within Chargify. Required, 
        /// unless a customer_reference or a set of customer_attributes is given.
        /// </summary>
        int? CustomerID { get; set; }

        /// <summary>
        /// The reference value (provided by your app) of an existing customer 
        /// within Chargify. Required, unless a customer_id or a set of 
        /// customer_attributes is given.
        /// </summary>
        string CustomerReference { get; set; }

        /// <summary>
        /// Details about the new customer
        /// </summary>
        CustomerAttributes CustomerAttributes { get; set; }

        /// <summary>
        /// The Payment Profile ID of an existing card or bank account, which belongs 
        /// to an existing customer to use for payment for this subscription
        /// </summary>
        int? PaymentProfileID { get; set; }

        /// <summary>
        /// Details of the payment profile to use with this new subscription
        /// </summary>
        PaymentProfileAttributes PaymentProfileAttributes { get; set; }

        /// <summary>
        /// Details of the credit card to use with this new subscription
        /// </summary>
        CreditCardAttributes CreditCardAttributes { get; set; }

        /// <summary>
        /// (Optional) Can be used when canceling a subscription (via the HTTP 
        /// DELETE method) to make a note about the reason for cancellation.
        /// </summary>
        string CancellationMessage { get; set; }

        /// <summary>
        /// (Optional) Set this attribute to a future date/time to sync imported subscriptions 
        /// to your existing renewal schedule.
        /// </summary>
        DateTime? NextBillingAt { get; set; }

        /// <summary>
        /// (Optional, default false) When set to true, and when next_billing_at is present, 
        /// if the subscription expires, the expires_at will be shifted by the same amount of 
        /// time as the difference between the old and new “next billing” dates.
        /// </summary>
        bool? ExpirationTracksNextBillingChange { get; set; }

        /// <summary>
        /// (Optional) Supplying the VAT number allows EU customer’s to opt-out of 
        /// the Value Added Tax assuming the merchant address and customer billing 
        /// address are not within the same EU country.
        /// </summary>
        string VatNumber { get; set; }

        /// <summary>
        /// (Optional) The coupon code of the coupon to apply
        /// </summary>
        string CouponCode { get; set; }

        /// <summary>
        /// (Optional) The type of payment collection to be used in the subscription. May be 
        /// automatic, or invoice.
        /// </summary>
        PaymentCollectionMethod? PaymentCollectionMethod { get; set; }

        /// <summary>
        /// The ACH agreements terms
        /// </summary>
        string AgreementTerms { get; set; }

        /// <summary>
        /// (Optional, used only for Delayed Product Change) When set to true, 
        /// indicates that a changed value for product_handle should schedule 
        /// the product change to the next subscription renewal.
        /// </summary>
        bool? ProductChangeDelayed { get; set; }

        /// <summary>
        /// A valid referral code. (optional, see Referrals for more details). 
        /// If supplied, must be valid, or else subscription creation will fail.
        /// </summary>
        string ReferralCode { get; set; }

        // TODO: Add this
        //CalendarBillingAttributes CalendarBilling { get; set; } 
        //Dictionary<string, string> Metafields { get; set; }
        //Dictionary<int, string> Components { get; set; }
    }

    [XmlType("subscription")]
    [Serializable]
    public class SubscriptionCreateOptions: ISubscriptionCreateOptions
    {
        /// <summary>
        /// The ACH agreements terms
        /// </summary>
        [XmlElement("agreement_terms")]
        public string AgreementTerms { get; set; }
        public bool ShouldSerializeAgreementTerms()
        {
            return !string.IsNullOrWhiteSpace(AgreementTerms);
        }

        /// <summary>
        /// (Optional) Can be used when canceling a subscription (via the HTTP 
        /// DELETE method) to make a note about the reason for cancellation.
        /// </summary>
        [XmlElement("cancellation_message")]
        public string CancellationMessage { get; set; }
        public bool ShouldSerializeCancellationMessage()
        {
            return !string.IsNullOrWhiteSpace(CancellationMessage);
        }

        /// <summary>
        /// (Optional) The coupon code of the coupon to apply
        /// </summary>
        [XmlElement("coupon_code")]
        public string CouponCode { get; set; }
        public bool ShouldSerializeCouponCode()
        {
            return !string.IsNullOrWhiteSpace(CouponCode);
        }

        /// <summary>
        /// Details about the new customer
        /// </summary>
        [XmlElement("customer_attributes")]
        public CustomerAttributes CustomerAttributes { get; set; }

        /// <summary>
        /// The ID of an existing customer within Chargify. Required, 
        /// unless a customer_reference or a set of customer_attributes is given.
        /// </summary>
        [XmlElement("customer_id")]
        public int? CustomerID { get; set; }
        public bool ShouldSerializeCustomerID()
        {
            return CustomerID.HasValue;
        }

        /// <summary>
        /// The reference value (provided by your app) of an existing customer 
        /// within Chargify. Required, unless a customer_id or a set of 
        /// customer_attributes is given.
        /// </summary>
        [XmlElement("customer_reference")]
        public string CustomerReference { get; set; }
        public bool ShouldSerializeCustomerReference()
        {
            return !string.IsNullOrWhiteSpace(CustomerReference);
        }

        /// <summary>
        /// (Optional, default false) When set to true, and when next_billing_at is present, 
        /// if the subscription expires, the expires_at will be shifted by the same amount of 
        /// time as the difference between the old and new “next billing” dates.
        /// </summary>
        [XmlElement("expiration_tracks_next_billing_change")]
        public bool? ExpirationTracksNextBillingChange { get; set; }
        public bool ShouldSerializeExpirationTracksNextBillingChange()
        {
            return ExpirationTracksNextBillingChange.HasValue;
        }

        /// <summary>
        /// (Optional) Set this attribute to a future date/time to sync imported subscriptions 
        /// to your existing renewal schedule.
        /// </summary>
        [XmlElement("next_billing_at")]
        public DateTime? NextBillingAt { get; set; }
        public bool ShouldSerializeNextBillingAt()
        {
            return NextBillingAt.HasValue;
        }

        /// <summary>
        /// (Optional) The type of payment collection to be used in the subscription. May be 
        /// automatic, or invoice.
        /// </summary>
        [XmlElement("payment_collection_method")]
        public PaymentCollectionMethod? PaymentCollectionMethod { get; set; }
        public bool ShouldSerializePaymentCollectionMethod()
        {
            return PaymentCollectionMethod.HasValue;
        }

        /// <summary>
        /// Details of the payment profile to use with this new subscription
        /// </summary>
        [XmlElement("payment_profile_attributes")]
        public PaymentProfileAttributes PaymentProfileAttributes { get; set; }

        /// <summary>
        /// Details of the credit card to use with this new subscription
        /// </summary>
        [XmlElement("credit_card_attributes")]
        public CreditCardAttributes CreditCardAttributes { get; set; }

        /// <summary>
        /// The Payment Profile ID of an existing card or bank account, which belongs 
        /// to an existing customer to use for payment for this subscription
        /// </summary>
        [XmlElement("payment_profile_id")]
        public int? PaymentProfileID { get; set; }
        public bool ShouldSerializePaymentProfileID()
        {
            return PaymentProfileID.HasValue;
        }

        /// <summary>
        /// (Optional, used only for Delayed Product Change) When set to true, 
        /// indicates that a changed value for product_handle should schedule 
        /// the product change to the next subscription renewal.
        /// </summary>
        [XmlElement("product_change_delayed")]
        public bool? ProductChangeDelayed { get; set; }
        public bool ShouldSerializeProductChangeDelayed()
        {
            return ProductChangeDelayed.HasValue;
        }

        /// <summary>
        /// The API Handle of the product for which you are creating a subscription. 
        /// Required, unless a product_id is given instead.
        /// </summary>
        [XmlElement("product_handle")]
        public string ProductHandle { get; set; }
        public bool ShouldSerializeProductHandle()
        {
            return !string.IsNullOrWhiteSpace(ProductHandle) && !ProductID.HasValue;
        }

        /// <summary>
        /// The Product ID of the product for which you are creating a subscription. 
        /// The product ID is not currently published, so we recommend using the API 
        /// Handle instead.
        /// </summary>
        [XmlElement("product_id")]
        public int? ProductID { get; set; }
        public bool ShouldSerializeProductID()
        {
            return ProductID.HasValue && string.IsNullOrWhiteSpace(ProductHandle);
        }

        /// <summary>
        /// A valid referral code. (optional, see Referrals for more details). 
        /// If supplied, must be valid, or else subscription creation will fail.
        /// </summary>
        [XmlElement("ref")]
        public string ReferralCode { get; set; }
        public bool ShouldSerializeReferralCode()
        {
            return !string.IsNullOrWhiteSpace(ReferralCode);
        }

        /// <summary>
        /// (Optional) Supplying the VAT number allows EU customer’s to opt-out of 
        /// the Value Added Tax assuming the merchant address and customer billing 
        /// address are not within the same EU country.
        /// </summary>
        [XmlElement("vat_number")]
        public string VatNumber { get; set; }
        public bool ShouldSerializeVatNumber()
        {
            return !string.IsNullOrWhiteSpace(VatNumber);
        }

        //[XmlElement("components")]
        //public Dictionary<int, string> Components { get; set; }

        //[XmlElement("metafields")]
        //public Dictionary<string, string> Metafields { get; set; }
    }
}