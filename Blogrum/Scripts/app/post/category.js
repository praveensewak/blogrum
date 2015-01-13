var vm = null;

var Category = function (Id, Name, UrlSlug, Description) {
    var self = this;

    /* Core Properties */
    self.Id = ko.observable(Id);
    self.Name = ko.protectedObservable(Name);
    self.UrlSlug = ko.protectedObservable(UrlSlug);
    self.Description = ko.protectedObservable(Description);

    /* Virtual Functions */
    self.edit = function (data) {
        vm.editCategoryItem(data);
    };

    /* Core Functions */
    self.commit = function () {
        if (self.Name.temp() == "" || self.Name.temp() == null) {
            swal({
                title: "Missing field!",
                text: "Please enter a name",
                type: "error"
            });
            return;
        }
        if (self.UrlSlug.temp() == "" || self.UrlSlug.temp() == null) {
            swal({
                title: "Missing field!",
                text: "Please enter a url slug",
                type: "error"
            });
            return;
        }
        self.Name.commit();
        self.UrlSlug.commit();
        self.Description.commit();

        vm.editCategoryItem(null);
    };

    self.reset = function () {
        self.Name.reset();
        self.UrlSlug.reset();
        self.Description.reset();

        vm.editCategoryItem(null);
    };
};

var ViewModel = function () {
    var self = this;

    /* Core Properties */
    self.Categories = ko.observableArray([]);

    /* Virtual Properties */
    self.editCategoryItem = ko.observable(null);

    /* Virtual Functions */
    self.addCategory = function (data) {
        var category = new Category(0, "Untitled", "", "");
        self.Categories.push(category);
        self.editCategoryItem(category);
    };
    self.deleteCategory = function (data) {
        self.Categories.remove(data);
        vm.editCategoryItem(null);
    };


    /* Core Function */

    self.loadData = function () {
        $.ajax({
            url: "/admin/categories/data",
            dataType: 'json',
            cache: false,
            type: "POST",
            error: function (data) {
                swal({
                    title: "Error!",
                    type: "error",
                    text: "There was an error loading categories",
                    confirmButtonText: "Bummer"
                });
            },
            success: function (data) {
                self.Categories([]);

                var categoryMapping = {
                    'Categories': {
                        key: function (data) {
                            return ko.utils.unwrapObservable(data.Id);
                        },
                        create: function (options) {
                            var category = options.data;

                            return new Category(category.Id, category.Name, category.UrlSlug, category.Description);
                        }
                    }
                };

                var model = ko.mapping.fromJS(data, categoryMapping);
                self.Categories(model.Categories());
            }
        });
    };

    self.saveData = function () {
        swal({
            title: "Confirm",
            text: "Do you want to save your changes?",
            type: "warning",
            showCancelButton: true,
            confirmButtonText: "Yes please!",
            cancelButtonText: "Wait!",
            closeOnConfirm: false,
            closeOnCancel: true
        }, function (isConfirm) {
            if (isConfirm) {
                var save = {
                    Categories: ko.toJS(self.Categories())
                };

                $.ajax({
                    url: "/admin/categories/save",
                    type: 'POST',
                    dataType: 'json',
                    data: JSON.stringify(save),
                    contentType: 'application/json; charset=utf-8',
                    cache: false,
                    success: function (data) {
                        if (data.Errors && data.Errors.length > 0) {
                            swal({
                                title: "Error!",
                                type: "error",
                                text: data.Errors.join(),
                                confirmButtonText: "Bummer"
                            });
                        } else {
                            swal({
                                title: "Success!",
                                type: "success",
                                text: "Categories saved successfully!",
                                confirmButtonText: "Woohoo!"
                            });

                            self.loadData();
                        }
                    },
                    error: function (data) {
                        swal({
                            title: "Error!",
                            type: "error",
                            text: "There was an error saving categories",
                            confirmButtonText: "Bummer"
                        });
                    }
                });
            }
        });
        
    };

    self.refreshData = function () {
        swal({
            title: "Confirm",
            text: "You will loose all unsaved changes!",
            type: "warning",
            showCancelButton: true,
            confirmButtonText: "Yes, refresh away!",
            cancelButtonText: "Woops, no don't!",
            closeOnConfirm: false,
            closeOnCancel: true
        }, function (isConfirm) {
            if (isConfirm) {
                self.loadData();
                swal("Refreshed!", "Reloaded categories from database", "success");
            }
        });
    };

    self.loadData();
};

$(function () {
    vm = new ViewModel();
    ko.applyBindings(vm);
});


