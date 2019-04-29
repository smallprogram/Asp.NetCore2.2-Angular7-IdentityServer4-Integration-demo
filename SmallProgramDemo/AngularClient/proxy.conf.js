const PROXY_CONFIG = [
    {
        context: [
            "/api",
            "/uploads",
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