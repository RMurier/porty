# ====== Build stage ======
FROM node:22-alpine AS build
WORKDIR /app

# Si tu as un lockfile, copie-le pour bénéficier du cache
COPY package.json package-lock.json* pnpm-lock.yaml* yarn.lock* ./
RUN npm ci || npm i

COPY . .
# Si ton code front utilise VITE_API_URL, définis-le à "/api" pour ce setup
ARG VITE_API_URL=/api
ENV VITE_API_URL=$VITE_API_URL

RUN npm run build

# ====== Runtime stage (Nginx) ======
FROM nginx:alpine
WORKDIR /usr/share/nginx/html
COPY --from=build /app/dist ./
# Conf Nginx pour proxy API + fallback SPA
COPY ./nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 80
