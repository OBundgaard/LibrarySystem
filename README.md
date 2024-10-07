To run the program simply run `docker compose up` or optionally `docker compose up -d` - perhaps include `--build`

First replica of the API will be on either port `5000` or `5001`
Second replica of the API will be on either port `5002` or `5003` - port of the first replica + 2 essentially

Include `/swagger/index.html` when trying to access the swagger UI

Optionally try through postman as it always works if done correctly and we've experienced inconsistent UI trouble with swagger across devices