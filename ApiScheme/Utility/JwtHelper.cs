using ApiScheme.Scheme;
using Newtonsoft.Json;
using paycircuit.com.google.iap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiScheme.Utility
{
    public static class JwtHelper
    {
        //const string SellerId = "04806629248295947480";
        //const string SellerSecret = "xzFzun3WgEG6nAc1x0rtOQ";

        public static string From(
            string SellerId,
            string SellerSecret,
            string userId,
            string sku,
            double amount,
            string name,
            string description,
            double price,
            double? recurrencePrice,
            string currencyCode)
        {
            JWTHeaderObject HeaderObj = new JWTHeaderObject(JWTHeaderObject.JWTHash.HS256, "1", "JWT");

            RequestObject request;
            if (recurrencePrice == null)
            {
                // Single item
                request = new InAppItemRequestObject()
                {
                    name = name,
                    description = description,
                    sellerData = JsonConvert.SerializeObject(new JwtSellerData() { userId = userId, sku = sku, amount = amount }),

                    currencyCode = currencyCode,
                    price = price.ToString()
                };
            }
            else
            {
                // Subscription
                request = new InAppItemSubscriptionRequestObject()
                {
                    name = name,
                    description = description,
                    sellerData = JsonConvert.SerializeObject(new JwtSellerData() { userId = userId, sku = sku, amount = amount }),

                    initialPayment = new InAppSubscriptionInitialPaymentObject()
                    {
                        currencyCode = currencyCode,
                        price = price.ToString(),
                        paymentType = "prorated"
                    },

                    recurrence = new InAppSubscriptionRecurrenceObject()
                    {
                        frequency = "monthly",
                        currencyCode = currencyCode,
                        numRecurrences = null,
                        price = recurrencePrice.ToString(),
                        recurrenceStartDate = DateTime.UtcNow.AddMonths(1)
                    }
                };
            }

            InAppItemObject ClaimObj = new InAppItemObject(request) { iss = SellerId };
            return JWTHelpers.buildJWT(HeaderObj, ClaimObj, SellerSecret);
        }
    }
}