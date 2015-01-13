//track an index on items in an observableArray
ko.observableArray.fn.indexed = function (prop) {
    prop = prop || 'index';
    //whenever the array changes, make one loop to update the index on each
    this.subscribe(function (newValue) {
        if (newValue) {
            var item;
            for (var i = 0, j = newValue.length; i < j; i++) {
                item = newValue[i];
                if (!ko.isObservable(item[prop])) {
                    item[prop] = ko.observable();
                }
                item[prop](i);
            }
        }
    });

    //initialize the index
    this.valueHasMutated();
    return this;
};

ko.bindingHandlers.flash = {
    init: function (element) {
        $(element).hide();
    },
    update: function (element, valueAccessor) {
        var value = ko.utils.unwrapObservable(valueAccessor());
        if (value) {
            $(element).stop().hide().text(value).fadeIn(function () {
                clearTimeout($(element).data("timeout"));
                $(element).data("timeout", setTimeout(function () {
                    $(element).fadeOut();
                    valueAccessor()(null);
                }, 3000));
            });
        }
    },
    timeout: null
};

ko.bindingHandlers.slideIn = {
    init: function (element) {
        $(element).hide();
    },
    update: function (element, valueAccessor) {
        var value = ko.utils.unwrapObservable(valueAccessor());
        if (value) {
            $(element).stop().hide().slideDown('fast');
        } else {
            $(element).stop().slideUp('fast');
        }
    }
};

ko.bindingHandlers.showModal = {
    init: function (element, valueAccessor) {
    },
    update: function (element, valueAccessor) {
        var value = valueAccessor();
        if (ko.utils.unwrapObservable(value)) {
            //$(element).modal('show');
            $(element).modal({
                show: true,
                backdrop: 'static',
                keyboard: false
            });
            // this is to focus input field inside dialog
            $("input", element).focus();
        }
        else {
            $(element).modal('hide');
        }

        //handle destroying an editor (based on what jQuery plugin does)
        ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
            $modal = $(element);
            if ($modal)
                $modal.modal('hide');
        });
    }
};

ko.bindingHandlers.showWindow = {
    init: function (element, valueAccessor) {

    },
    update: function (element, valueAccessor) {
        var value = valueAccessor();
        var win = $(element).data('tWindow');
        if (ko.utils.unwrapObservable(value)) {
            win.open();
        } else {
            win.close();
        }

        ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
            if ($(element))
                win.destroy();
        });
    }
};

ko.bindingHandlers.bootstrapTab = {
    init: function (element, valueAccessor) {
    },
    update: function (element, valueAccessor) {
        var value = valueAccessor();
        if (ko.utils.unwrapObservable(value)) {
            $('a:first', element).tab('show');

            $('a', element).on('click', function (e) {
                e.preventDefault();
                $(this).tab('show');
            });
        }
    }
};

// wrapper to an observable that requires accept/cancel
ko.protectedObservable = function (initialValue) {
    // private variables
    var _actualValue = ko.observable(initialValue),
        _tempValue = initialValue;

    // computed observable that we will return
    var result = ko.computed({
        // always return the actual value
        read: function () {
            return _actualValue();
        },
        // stored in a temporary spo until commit
        write: function (newValue) {
            _tempValue = newValue;
        }
    });

    // if different, commit temp value
    result.commit = function () {
        if (_tempValue !== _actualValue()) {
            _actualValue(_tempValue);
        }
    };

    // force subscribers to take original
    result.reset = function () {
        _actualValue.valueHasMutated();
        _tempValue = _actualValue(); // reset temp value
    };

    // get entered values
    result.temp = function () {
        return _tempValue;
    };

    return result;
};

// wrapper to an observable tha requires numeric input
ko.numericObservable = function (initialValue) {
    var _actual = ko.observable(initialValue);

    var result = ko.dependentObservable({
        read: function () {
            return _actual();
        },
        write: function (newValue) {
            var parsed = parseFloat(newValue);
            _actual(isNaN(parsed) ? newValue : parsed);
        }
    });

    return result;
};

// wrapper to an observable that requires paging
ko.observableArray.fn.paged = function (perPage) {
    var items = this;

    items.current = ko.observable(1);
    items.perPage = perPage;
    items.pagedItems = ko.computed(function () {
        var pg = this.current(),
            start = this.perPage * (pg - 1),
            end = start + this.perPage;
        return this().slice(start, end);
    }, items);

    items.next = function () {
        if (this.next.enabled())
            this.current(this.current() + 1);
    }.bind(this);

    items.next.enabled = ko.computed(function () {
        return this().length > this.perPage * this.current();
    }, items);

    items.prev = function () {
        if (this.prev.enabled())
            this.current(this.current() - 1);
    }.bind(this);

    items.prev.enabled = ko.computed(function () {
        return this.current() > 1;
    }, items);


    return items;
};


function formatCurrency(value) {
    return "$" + parseFloat(value).toFixed(2);
}