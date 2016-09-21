var BeerPicker = {
    selectedStyleId: -1,
    selectedSeason: -1,
    selectedGlassware: '',
    isOrganic: false,
    isLabel: false,

    SetStyle: function (id, name) {
        this.selectedStyleId = id;
        $('#pickedStyle').html(name);
        this.ScrollTo($('#PickSeason'));
    },

    SetSeason: function (id, name) {
        this.selectedSeason = id;
        $('#pickedSeason').html(name);
        this.ScrollTo($('#pickGlassware'));
    },

    SetGlassware: function (id, name){
        this.selectedGlassware = id;
        $('#pickedGlassware').html(name);
        this.ScrollTo($('#pickOrganic'));
    },

    SetOrganic: function (val){
        this.isOrganic = val;
        if(val == true)
            $('#pickedOrganic').html('Organic');
        else
            $('#pickedOrganic').html('Not organic');
        this.ScrollTo($('#pickLabel'));
    },

    SetLabel: function (val) {
        this.isLabel = val;
        if (val == true)
            $('#pickedLabel').html('With label');
        else
            $('#pickedLabel').html('With no label');
        this.ScrollTo($('#pickLabel'));
        $('#Go').show();
    },

    GO: function(){
        $.get(
            'Home/Results',
            {
                styleId: this.selectedStyleId,
                seasonId: this.selectedSeason,
                glasswareId: this.selectedGlassware,
                isOrganic: this.isOrganic,
                isLabel: this.isLabel
            },
            function (data, status, xhr) {
                $('#SearchResults').show().html(data);

                BeerPicker.ScrollTo($('#SearchResults'));
            }
        );
    },

    ScrollTo: function (ele) {
        $('html, body').animate({
            scrollTop: $(ele).offset().top - 150
        }, 500);
    }
};