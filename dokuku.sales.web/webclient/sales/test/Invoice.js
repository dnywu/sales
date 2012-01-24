var DOKUKU = {};

DOKUKU.Invoice = function (customerId, poNumber) {
    this.customerId = customerId;
    this.poNumber = poNumber;
};

DOKUKU.Invoice.prototype.add = function (item) {
};

DOKUKU.InvoiceItem = function (itemId, desc, qty, rate) {
    this.itemId = itemId;
    this.desc = desc;
    this.qty = qty;
    this.rate = rate;
};