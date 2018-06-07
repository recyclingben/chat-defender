// Wrapper around 'require'. Returns a promise that will resolve
// once the file is loaded.
const acquire = (...items) => 
    new Promise(success => {
        require(items, (...results) =>
            results.length === 1 
                ? success(results[0])
                : success(results)
        );
    });

// testing 'acquire' method
(async () => {
    let player = await acquire('player');
    console.log(player);

    let player2 = await acquire('player', 'uglyperson');
    console.log(player2);
})();