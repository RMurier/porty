import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";
import fs from "node:fs";

export default defineConfig({
  plugins: [react()],
  server: {
    host: true,
    port: 443,
    proxy: {
      "/api": {
        target: "http://api:8080",
        changeOrigin: true,
      },
    },
  },
});
