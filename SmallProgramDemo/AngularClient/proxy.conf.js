const PROXY_CONFIG = [
    {
        context: [
            "/api"
            // "/many",
            // "/endpoints",
            // "/i",
            // "/need",
            // "/to",
            // "/proxy"
        ],
        target: "https://localhost:6001",
        secure: false
    }
]
module.exports = PROXY_CONFIG;