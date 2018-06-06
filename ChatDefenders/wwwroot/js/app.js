const acquire = item => {
    return new Promise(function (success, reject) {
        require([item], function (i) {
            success(i);
        });
    });
}

!async function () {

    let player = await acquire('player');
    console.log(player);

    let player2 = require('player');
    console.log(player2);

}();