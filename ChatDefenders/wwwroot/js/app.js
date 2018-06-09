; (() => {
    // Wrapper around 'require'. Returns a promise that will resolve
    // once the file is loaded.
    const acquire = (...items) =>
        new Promise(success => {
            require(items, (...results) =>
                results.length === 1
                    ? success(results[0])
                    : success(results)
            )
        })
    ;(async () => {
        console.log(await acquire('player'))
    }) ()
})()