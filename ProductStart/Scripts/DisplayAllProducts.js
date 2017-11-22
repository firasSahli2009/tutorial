var ViewModel = function () {
    var self = this;
    self.products = ko.observableArray();
    self.error = ko.observable();

    self.getProductDetail = function (item) {
        ajaxHelper(productssUri + item.Id, 'GET').done(function (data) {
            self.detail(data);
        });
    }




    var productsUri = '/api/products/';

    function ajaxHelper(uri, method, data) {
        self.error(''); // Clear error message
        return $.ajax({
            type: method,
            url: uri,
            dataType: 'json',
            contentType: 'application/json',
            data: data ? JSON.stringify(data) : null
        }).fail(function (jqXHR, textStatus, errorThrown) {
            self.error(errorThrown);
        });
    }

    function getAllProductss() {
        ajaxHelper(productsUri, 'GET').done(function (data) {
            self.products(data);
        });
    }

    // Fetch the initial data.
    getAllProducts();
};

ko.applyBindings(new ViewModel());