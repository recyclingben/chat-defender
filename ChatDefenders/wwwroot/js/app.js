// Returns a promise that resolves once 'item' is loaded.
// This code is beautiful, but also kind of evil for those
// trying to read it.
const acquire = (...items) => 
    new Promise(success => {
        require(items, (...results) =>
            results.length === 1 
                ? success(results[0])
                : success(results)
        );
    });

(async () => {
    let player = await acquire('player');
    console.log(player);

    let player2 = await acquire('player', 'uglyperson');
    console.log(player2);
})();