import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";
import fs from "node:fs";

export default defineConfig({
  plugins: [react()],
  server: {
    host: true,
    port: 443,
    https: {
      cert: fs.readFileSync("/https/cert.crt"),
      key: fs.readFileSync("/https/cert.key"),
    },
    proxy: {
      "/api": {
        target: "http://api:8080",
        changeOrigin: true,
      },
    },
  },
});
