// Returns a promise that resolves once 'item' is loaded.
const acquire = (...items) => 
    new Promise(success => {
        require(items, (...results) => {
            if (results.length === 1) success(results[0]);
            else success(results);
        });
    });

(async () => {
    let player = await acquire('player');
    console.log(player);

    let player2 = await acquire('player', 'uglyperson');
    console.log(player2);
})();