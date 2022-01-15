const (
	MIN_LENGTH = 3
	MAX_LENGTH = 40
)

type UserName struct {
	value string
}

func NewUserName(name string) UserName {
	if len(name) < MIN_LENGTH {
		error.FromMinLength("email", MIN_LENGTH)
	}

	if len(name) > MAX_LENGTH {
		error.FromMaxLength("email", MIN_LENGTH)
	}

	return UserName{value: name}
}

func (name UserName) Value() string {
	return name.value
}